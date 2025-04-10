using ChickTrack.Domain.DataTransferObjects.Financial.CreateDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;
using ChickTrack.Domain.Entities.Financials;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Services.Interface;
namespace ChickTrack.Service.Interfaces.Financial
{
    public interface ISaleRecordService : IMSSQLBaseService<SaleRecord, long>
    {
        Task<Result<SaleRecordDto>> CreateSalesRecord(CreateSaleRecordDto saleRecordDto);
    }
}
