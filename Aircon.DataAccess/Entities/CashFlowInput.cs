using Dapper.Contrib.Extensions;

namespace Aircon.DataAccess.Entities
{
    [Table("CashFlowInput")]
    public class CashFlowInput
    {
        [Key]
        public int CashFlowInputId { get; set; }
        public int TransactionInputId { get; set; }
        public decimal Amount { get; set; }
        public int Period { get; set; }
    }

}
