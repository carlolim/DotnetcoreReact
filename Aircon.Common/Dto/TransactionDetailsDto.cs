using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.Common.Dto
{
    public class TransactionDetailsDto
    {
        public double LowerBoundDiscount { get; set; }
        public double UpperBoundDiscount { get; set; }
        public double DiscountIncrement { get; set; }
        public decimal Amount { get; set; }
        public List<CashFlowInputDto> CashFlowInputs { get; set; }
        public List<TransactionResultDto> Result { get; set; }
    }
}
