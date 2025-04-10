using ChickTrack.Domain.DataTransferObjects.Financial.CreateDtos;

namespace ChickTrack.Domain.DataTransferObjects.Financial.UpdateDtos
{
    public class UpdateSalesRecordDto : CreateSaleRecordDto
    {
        public long Id { get; set; }
    }
}
