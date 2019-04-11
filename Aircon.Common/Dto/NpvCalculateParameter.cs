using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.Common.Dto
{
    public class NpvCalculateParameter
    {
        public double LowerBoundDiscount { get; set; }
        public double UpperBoundDiscount { get; set; }
        public double DiscountIncrement { get; set; }
        public decimal Amount { get; set; }
        public List<CashFlowInputDto> CashFlows { get; set; }
    }
}
