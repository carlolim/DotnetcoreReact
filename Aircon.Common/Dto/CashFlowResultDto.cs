using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.Common.Dto
{
    public class CashFlowResultDto
    {
        public List<CashFlow> CashFlows { get; set; }
        public double DiscountRate { get; set; }
        public decimal NetPresentValue { get; set; }
    }
}
