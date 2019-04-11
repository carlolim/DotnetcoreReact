using System.Collections.Generic;
using System.Threading.Tasks;
using Aircon.Common;
using Aircon.DataAccess.Entities;

namespace Aircon.DataAccess
{
    public interface ITransactionInputDataAccess
    {
        Task<IEnumerable<TransactionInput>> All();
        Task<TransactionInput> ById(int id);
        Task<Result> Insert(TransactionInput data);
    }
}