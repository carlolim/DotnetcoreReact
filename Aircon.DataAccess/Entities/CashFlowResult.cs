using Dapper.Contrib.Extensions;

namespace Aircon.DataAccess.Entities
{
    [Table("CashFlowResult")]
    public class CashFlowResult
    {
        [Key]
        public int CashFlowResultId { get; set; }
        public int TransactionResultId { get; set; }
        public int TransactionInputId { get; set; }
        public int Period { get; set; }
        public decimal Amount { get; set; }
        public decimal Value { get; set; }
    }

}
