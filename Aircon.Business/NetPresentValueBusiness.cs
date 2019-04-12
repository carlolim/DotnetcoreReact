using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Aircon.Common;
using Aircon.Common.Dto;
using Aircon.DataAccess;
using Aircon.DataAccess.Entities;

namespace Aircon.Business
{
    public class NetPresentValueBusiness : INetPresentValueBusiness
    {
        private readonly IDbContext _dbContext;

        public NetPresentValueBusiness(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Add(NpvCalculateParameter data)
        {
            var result = new Result();
            var calculateResult = Calculate(data).ToList();
            if (calculateResult.Count() > 0)
            {
                #region Prepare data for database transacions
                var cashFlowResults = new List<CashFlowResult>();
                var cashFlowInputs = data.CashFlows.Select(m => new CashFlowInput
                {
                    Amount = m.Amount,
                    Period = m.Period,
                    TransactionInputId = 0
                }).ToList();
                var transactionInput = new TransactionInput
                {
                    Amount = data.Amount,
                    DiscountRate = data.DiscountIncrement,
                    UpperBoundDiscount = data.UpperBoundDiscount,
                    LowerBoundDiscount = data.LowerBoundDiscount,
                    DateAdded = DateTime.Now
                };
                var transactionResult = calculateResult.Select(m => new TransactionResult
                {
                    NetPresentValue = m.NetPresentValue,
                    DiscountRate = m.DiscountRate
                }).ToList();
                #endregion

                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var transactionInputInsertResult = await _dbContext.TransactionInputDataAccess.Insert(transactionInput);
                        cashFlowInputs = cashFlowInputs.Select(m => { m.TransactionInputId = transactionInputInsertResult.Id; return m; }).ToList();
                        var cashFlowInputInsertResult = _dbContext.CashFlowInputDataAccess.BulkInsert(cashFlowInputs);

                        foreach (var calcResult in calculateResult)
                        {
                            var transactionResultForInsert = new TransactionResult
                            {
                                DiscountRate = calcResult.DiscountRate,
                                NetPresentValue = calcResult.NetPresentValue,
                                TransactionInputId = transactionInputInsertResult.Id
                            };

                            var transResultInsertResult = await _dbContext.TransactionResultDataAccess.Insert(transactionResultForInsert);
                            if (!transResultInsertResult.IsSuccess)
                                throw new Exception(transResultInsertResult.Message);

                            cashFlowResults.AddRange(calcResult.CashFlows.Select(m => new CashFlowResult
                            {
                                Amount = m.Amount,
                                Period = m.Period,
                                TransactionResultId = transResultInsertResult.Id,
                                Value = m.Value
                            }));
                        }

                        var cashflowResultInsertResult = _dbContext.CashFlowResultDataAccess.BulkInsert(cashFlowResults);

                        if (!cashFlowInputInsertResult.IsSuccess ||
                            !cashflowResultInsertResult.IsSuccess ||
                            !transactionInputInsertResult.IsSuccess)
                            throw new Exception();


                        transactionScope.Complete();
                        result.IsSuccess = true;
                    }
                    catch (Exception ex) //TODO: logger
                    {
                        calculateResult = new List<TransactionResultDto>();
                        result.IsSuccess = false;
                    }
                }
            }
            return result;
        }

        public async Task<IEnumerable<TransactionInput>> All()
        {
            return await _dbContext.TransactionInputDataAccess.All();
        }

        public async Task<TransactionDetailsDto> DetailsById(int transactionId)
        {
            var result = new TransactionDetailsDto();
            var transResults = new List<TransactionResultDto>();

            var transactionInput = await _dbContext.TransactionInputDataAccess.ById(transactionId);
            var transactionResults = await _dbContext.TransactionResultDataAccess.ByTransactionInputId(transactionId);
            var cashFlowInputs = await _dbContext.CashFlowInputDataAccess.ByTransactionInputId(transactionId);
            var cashFlowResults = await _dbContext.CashFlowResultDataAccess.ByTransactionInputId(transactionId);

            foreach (var transResult in transactionResults)
            {
                var cashflows = cashFlowResults.Where(m => m.TransactionResultId == transResult.TransactionResultId).Select(m => new CashFlowResultDto
                {
                    Amount = m.Amount,
                    Period = m.Period,
                    Value = m.Value
                }).ToList();
                transResults.Add(new TransactionResultDto
                {
                    DiscountRate = transResult.DiscountRate,
                    NetPresentValue = transResult.NetPresentValue,
                    CashFlows = cashflows
                });
            }

            result.Amount = transactionInput.Amount;
            result.DiscountIncrement = transactionInput.DiscountRate;
            result.LowerBoundDiscount = transactionInput.LowerBoundDiscount;
            result.UpperBoundDiscount = transactionInput.UpperBoundDiscount;
            result.CashFlowInputs = cashFlowInputs.Select(m => new CashFlowInputDto { Amount = m.Amount, Period = m.Period }).ToList();
            result.Result = transResults;

            return result;
        }

        public IEnumerable<TransactionResultDto> Calculate(NpvCalculateParameter data)
        {
            for (var discount = data.LowerBoundDiscount;
                discount <= data.UpperBoundDiscount;
                discount += data.DiscountIncrement)
            {
                var cashFlowResults = new List<CashFlowResultDto>();
                foreach (var cashFlow in data.CashFlows)
                {
                    var cashFlowAmountDouble = Convert.ToDouble(cashFlow.Amount);
                    var value = cashFlowAmountDouble / Math.Pow((1 + (discount / 100)), cashFlow.Period);
                    cashFlowResults.Add(new CashFlowResultDto()
                    {
                        Period = cashFlow.Period,
                        Amount = cashFlow.Amount,
                        Value = Convert.ToDecimal(value)
                    });
                }
                yield return new TransactionResultDto
                {
                    CashFlows = cashFlowResults,
                    DiscountRate = discount,
                    NetPresentValue = cashFlowResults.Sum(m => m.Value) - data.Amount
                };
                if (data.DiscountIncrement.Equals(0)) break;
            }
        }
    }
}
