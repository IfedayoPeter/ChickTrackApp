using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;

namespace ChickTrack.Domain.DataTransferObjects.Financial.UpdateDtos
{
    public class UpdateInvestmentDto : CreateInvestmentDto
    {
        public long Id { get; set; }
    }
}
