using Aircon.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.DataAccess
{
    public class DbContext : IDbContext
    {
        public ICashFlowInputDataAccess CashFlowInputDataAccess { get; set; }
        public IGenericBase<CashFlowResult> CashFlowResultDataAccess { get; set; }
        public IGenericBase<TransactionInput> TransactionInputDataAccess { get; set; }
        public IGenericBase<TransactionResult> TransactionResultDataAccess { get; set; }

        public DbContext(ICashFlowInputDataAccess cashFlowInputDataAccess,
            IGenericBase<CashFlowResult> cashFlowResultDataAccess,
            IGenericBase<TransactionInput> transactionInputDataAccess,
            IGenericBase<TransactionResult> transactionResultDataAccess
            )
        {
            CashFlowInputDataAccess = cashFlowInputDataAccess;
            CashFlowResultDataAccess = cashFlowResultDataAccess;
            TransactionInputDataAccess = transactionInputDataAccess;
            TransactionResultDataAccess = transactionResultDataAccess;
        }
    }
}
