using System;
using Dapper.Contrib.Extensions;

namespace Aircon.DataAccess.Entities
{
    [Table("TransactionInput")]
    public class TransactionInput
    {
        [Key]
        public int TransactionInputId { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal LowerBoundDiscount { get; set; }
        public decimal UpperBoundDiscount { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal Amount { get; set; }
        public decimal Value { get; set; }
    }

}
