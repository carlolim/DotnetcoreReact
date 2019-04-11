using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Aircon.Common;
using Aircon.Common.AppSettings;
using Aircon.DataAccess.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;

namespace Aircon.DataAccess
{
    public class CashFlowInputDataAccess
    {
        private readonly string _connectionString;
        public CashFlowInputDataAccess(IOptions<DatabaseConnections> connectionStrings)
        {
            _connectionString = connectionStrings.Value.MainDatabase;
        }

        public async Task<IEnumerable<CashFlowInput>> All()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
                return await db.QueryAsync<CashFlowInput>("SELECT * FROM [CashFlowInput]");
        }

        public async Task<CashFlowInput> ById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                SqlParameter parameter = new SqlParameter("@Id", id);
                return await db.QueryFirstOrDefaultAsync<CashFlowInput>(
                    "SELECT * FROM [CashFlowInput] WHERE CashFlowInputId = @Id", parameter);
            }
        }

        public async Task<CashFlowInput> ByTransactionInputId(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                SqlParameter parameter = new SqlParameter("@Id", id);
                return await db.QueryFirstOrDefaultAsync<CashFlowInput>(
                    "SELECT * FROM [CashFlowInput] WHERE TransactionInputId = @Id", parameter);
            }
        }

        public async Task<Result> Insert(CashFlowInput data)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var result = await db.InsertAsync(data);
                return new Result { IsSuccess = result > 0, Id = result };
            }
        }
    }
}
