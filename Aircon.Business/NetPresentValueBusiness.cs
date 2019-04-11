using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<CashFlowResultDto>> Add(NpvCalculateParameter data)
        {
            var calculateResult = Calculate(data);
            if (calculateResult.Count() > 0)
            {
                List<CashFlowInput> cashFlowInputs = data.CashFlows.Select(m => new CashFlowInput
                {
                    Amount = m.Amount,
                    Period = m.Period,
                    TransactionInputId = 1
                }).ToList();
            }

            return null;
        }

        public IEnumerable<CashFlowResultDto> Calculate(NpvCalculateParameter data)
        {
            for (var discount = data.LowerBoundDiscount;
                discount <= data.UpperBoundDiscount;
                discount += data.DiscountIncrement)
            {
                var cashFlowResults = new List<CashFlow>();
                foreach (var cashFlow in data.CashFlows)
                {
                    var cashFlowAmountDouble = Convert.ToDouble(cashFlow.Amount);
                    var value = cashFlowAmountDouble / Math.Pow((1 + (discount / 100)), cashFlow.Period);
                    cashFlowResults.Add(new CashFlow()
                    {
                        Period = cashFlow.Period,
                        Amount = cashFlow.Amount,
                        Value = Convert.ToDecimal(value)
                    });
                }
                yield return new CashFlowResultDto
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
