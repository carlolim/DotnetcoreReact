using System.Collections.Generic;
using System.Threading.Tasks;
using Aircon.DataAccess.Entities;

namespace Aircon.DataAccess
{
    public interface ITransactionResultDataAccess : IGenericBase<TransactionResult>
    {
        Task<IEnumerable<TransactionResult>> ByTransactionInputId(int id);
    }
}