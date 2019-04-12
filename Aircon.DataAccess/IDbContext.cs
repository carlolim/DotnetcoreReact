using Aircon.DataAccess.Entities;

namespace Aircon.DataAccess
{
    public interface IDbContext
    {
        ICashFlowInputDataAccess CashFlowInputDataAccess { get; set; }
        ICashFlowResultDataAccess CashFlowResultDataAccess { get; set; }
        IGenericBase<TransactionInput> TransactionInputDataAccess { get; set; }
        ITransactionResultDataAccess TransactionResultDataAccess { get; set; }
    }
}