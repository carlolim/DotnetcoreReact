using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.Common.Dto
{
    public class TransactionResultDto
    {
        public List<CashFlowResultDto> CashFlows { get; set; }
        public double DiscountRate { get; set; }
        public decimal NetPresentValue { get; set; }
    }
}
