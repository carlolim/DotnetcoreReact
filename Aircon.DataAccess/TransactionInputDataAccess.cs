using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aircon.Common;
using Aircon.Common.AppSettings;
using Aircon.DataAccess.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace Aircon.DataAccess
{
    public class TransactionInputDataAccess : ITransactionInputDataAccess
    {
        private readonly string _connectionString;
        public TransactionInputDataAccess(IOptions<DatabaseConnections> connectionStrings)
        {
            _connectionString = connectionStrings.Value.MainDatabase;
        }

        public async Task<IEnumerable<TransactionInput>> All()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
                return await db.QueryAsync<TransactionInput>("SELECT * FROM [TransactionInput]");
        }

        public async Task<TransactionInput> ById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                SqlParameter parameter = new SqlParameter("@Id", id);
                return await db.QueryFirstOrDefaultAsync<TransactionInput>(
                    "SELECT * FROM [TransactionInput] WHERE TransactionInputId = @Id", parameter);
            }
        }

        public async Task<Result> Insert(TransactionInput data)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var result = await db.InsertAsync(data);
                return new Result { IsSuccess = result > 0, Id = result };
            }
        }
    }
}
