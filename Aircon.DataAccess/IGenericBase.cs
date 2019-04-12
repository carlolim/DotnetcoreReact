using System.Collections.Generic;
using System.Threading.Tasks;
using Aircon.Common;

namespace Aircon.DataAccess
{
    public interface IGenericBase<T> where T : class
    {
        string ConnectionString { get; }

        Task<IEnumerable<T>> All();
        Task<T> ById(int id);
        Task<Result> Delete(T data);
        Task<Result> Insert(T data);
        Task<Result> Update(T data);
        Result BulkInsert(List<T> data);
    }
}