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
        public double LowerBoundDiscount { get; set; }
        public double UpperBoundDiscount { get; set; }
        public double DiscountRate { get; set; }
        public decimal Amount { get; set; }
    }

}
