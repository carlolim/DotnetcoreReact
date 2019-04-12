using Aircon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.DataAccess
{
    public class DbContext : IDbContext
    {
        public ICashFlowInputDataAccess CashFlowInputDataAccess { get; set; }
        public ICashFlowResultDataAccess CashFlowResultDataAccess { get; set; }
        public IGenericBase<TransactionInput> TransactionInputDataAccess { get; set; }
        public ITransactionResultDataAccess TransactionResultDataAccess { get; set; }

        public DbContext(ICashFlowInputDataAccess cashFlowInputDataAccess,
            ICashFlowResultDataAccess cashFlowResultDataAccess,
            IGenericBase<TransactionInput> transactionInputDataAccess,
            ITransactionResultDataAccess transactionResultDataAccess
            )
        {
            CashFlowInputDataAccess = cashFlowInputDataAccess;
            CashFlowResultDataAccess = cashFlowResultDataAccess;
            TransactionInputDataAccess = transactionInputDataAccess;
            TransactionResultDataAccess = transactionResultDataAccess;
        }
    }
}
