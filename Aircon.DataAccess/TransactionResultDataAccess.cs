using Aircon.Common.AppSettings;
using Aircon.DataAccess.Entities;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.DataAccess
{
    public class TransactionResultDataAccess : GenericBase<TransactionResult>, ITransactionResultDataAccess
    {
        public TransactionResultDataAccess(IOptions<DatabaseConnections> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<TransactionResult>> ByTransactionInputId(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                SqlParameter parameter = new SqlParameter("@Id", id);
                return await db.QueryAsync<TransactionResult>(
                    "SELECT * FROM [TransactionResult] WHERE TransactionInputId = @Id", parameter);
            }
        }
    }
}
