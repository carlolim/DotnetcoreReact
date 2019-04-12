using System.Collections.Generic;
using System.Threading.Tasks;
using Aircon.DataAccess.Entities;

namespace Aircon.DataAccess
{
    public interface ICashFlowResultDataAccess : IGenericBase<CashFlowResult>
    {
        Task<IEnumerable<CashFlowResult>> ByTransactionInputId(int id);
    }
}