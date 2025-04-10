using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;

namespace ChickTrack.Domain.DataTransferObjects.Financial.UpdateDtos
{
    public class UpdateInvestmentSummaryDto : CreateInvestmentSummaryDto
    {
        public long Id { get; set; }
    }
}
