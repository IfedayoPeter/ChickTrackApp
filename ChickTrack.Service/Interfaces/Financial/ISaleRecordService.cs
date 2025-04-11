using ChickTrack.Domain.DataTransferObjects.Financial.CreateDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.UpdateDtos;
using ChickTrack.Domain.Entities.Financials;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Services.Interface;
namespace ChickTrack.Service.Interfaces.Financial
{
    public interface ISaleRecordService : IMSSQLBaseService<SaleRecord, long>
    {
        Task<Result<SaleRecordDto>> CreateAsync(CreateSaleRecordDto saleRecordDto);
        Task<Result<SaleRecordDto>> UpdateAsync(long id, UpdateSalesRecordDto saleRecordDto);
        Task<Result<bool>> DeleteAsync(long id);
    }
}
