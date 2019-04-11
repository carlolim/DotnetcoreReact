using System.Collections.Generic;
using Aircon.Common.Dto;

namespace Aircon.Business
{
    public interface INetPresentValueBusiness
    {
        IEnumerable<CashFlowResultDto> Calculate(NpvCalculateParameter data);
    }
}