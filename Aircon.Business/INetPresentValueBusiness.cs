using System.Collections.Generic;
using System.Threading.Tasks;
using Aircon.Common;
using Aircon.Common.Dto;
using Aircon.DataAccess.Entities;

namespace Aircon.Business
{
    public interface INetPresentValueBusiness
    {
        Task<Result> Add(NpvCalculateParameter data);
        IEnumerable<TransactionResultDto> Calculate(NpvCalculateParameter data);
        Task<IEnumerable<TransactionInput>> All();
        Task<TransactionDetailsDto> DetailsById(int transactionId);
    }
}