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
    public class CashFlowResultDataAccess : GenericBase<CashFlowResult>, ICashFlowResultDataAccess
    {
        public CashFlowResultDataAccess(IOptions<DatabaseConnections> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<CashFlowResult>> ByTransactionInputId(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                SqlParameter parameter = new SqlParameter("@Id", id);
                return await db.QueryAsync<CashFlowResult>(
                    "SELECT * FROM [CashFlowResult] WHERE TransactionInputId = @Id", parameter);
            }
        }
    }
}
