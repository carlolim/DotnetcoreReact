using Dapper.Contrib.Extensions;

namespace Aircon.DataAccess.Entities
{
    [Table("TransactionResult")]
    public class TransactionResult
    {
        [Key]
        public int TransactionResultId { get; set; }
        public double DiscountRate { get; set; }
        public decimal NetPresentValue { get; set; }
        public int TransactionInputId { get; set; }
    }

}
