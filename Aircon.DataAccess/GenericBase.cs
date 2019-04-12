using Aircon.Common.AppSettings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Aircon.Common;
using Z.Dapper.Plus;

namespace Aircon.DataAccess
{
    public class GenericBase<T> : IGenericBase<T> where T : class
    {
        public string ConnectionString { get; set; }
        public GenericBase(IOptions<DatabaseConnections> connectionStrings)
        {
            ConnectionString = connectionStrings.Value.MainDatabase;
        }

        //all(ok), by id(ok), insert(ok), update(ok), delete(ok), bulk insert, bulk update, bulk delete
        public async Task<IEnumerable<T>> All()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
                return await db.GetAllAsync<T>();
        }

        public async Task<T> ById(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
                return await db.GetAsync<T>(id);
        }

        public async Task<Result> Insert(T data)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = await db.InsertAsync(data);
                return new Result { Id = result, IsSuccess = result > 0 };
            }
        }

        public async Task<Result> Update(T data)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = await db.UpdateAsync(data);
                return new Result { IsSuccess = result };
            }
        }

        public async Task<Result> Delete(T data)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = await db.DeleteAsync(data);
                return new Result { IsSuccess = result };
            }
        }

        public Result BulkInsert(List<T> data)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var result = db.BulkInsert(data);
                return new Result { IsSuccess = result.CurrentItem.Any() };
            }
        }
    }
}
