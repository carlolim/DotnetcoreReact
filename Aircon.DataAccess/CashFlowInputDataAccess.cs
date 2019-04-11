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
    public class CashFlowInputDataAccess : GenericBase<CashFlowInput>, ICashFlowInputDataAccess
    {
        public CashFlowInputDataAccess(IOptions<DatabaseConnections> connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<CashFlowInput>> ByTransactionInputId(int id)
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                SqlParameter parameter = new SqlParameter("@Id", id);
                return await db.QueryAsync<CashFlowInput>(
                    "SELECT * FROM [CashFlowInput] WHERE TransactionInputId = @Id", parameter);
            }
        }
    }
}
