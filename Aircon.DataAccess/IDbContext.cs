using Aircon.DataAccess.Entities;

namespace Aircon.DataAccess
{
    public interface IDbContext
    {
        ICashFlowInputDataAccess CashFlowInputDataAccess { get; set; }
        IGenericBase<CashFlowResult> CashFlowResultDataAccess { get; set; }
        IGenericBase<TransactionInput> TransactionInputDataAccess { get; set; }
        IGenericBase<TransactionResult> TransactionResultDataAccess { get; set; }
    }
}