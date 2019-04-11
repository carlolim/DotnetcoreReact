using System.Collections.Generic;
using System.Threading.Tasks;
using Aircon.DataAccess.Entities;

namespace Aircon.DataAccess
{
    public interface ICashFlowInputDataAccess : IGenericBase<CashFlowInput>
    {
        Task<IEnumerable<CashFlowInput>> ByTransactionInputId(int id);
    }
}