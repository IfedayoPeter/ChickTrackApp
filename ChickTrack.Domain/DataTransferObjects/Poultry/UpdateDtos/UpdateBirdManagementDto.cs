using ChickTrack.Domain.Entities.Poultry;
using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class UpdateBirdManagementDto : CreateBirdManagementDto
    {
        public long Id { get; set; }
    }
}
