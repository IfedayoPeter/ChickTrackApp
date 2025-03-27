using ChickTrack.Domain.DataTransferObjects.Financial;
using ChickTrack.Domain.Entities.Financials;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Services.Interface;
namespace ChickTrack.Service.Interfaces.Financial
{
    public interface ISaleRecordService : IMSSQLBaseService<SaleRecord, long>
    {
        Task<Result<SaleRecordDto>> CreateSalesRecord(SaleRecordDto saleRecordDto);
    }
}
