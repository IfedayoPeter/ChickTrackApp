using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Entities.Poultry;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces.Poultry
{
    public interface IBirdManagementService : IMSSQLBaseService<BirdManagement, long>
    {
        Task<Result<BirdManagementDto>> CreateBirdManagement(CreateBirdManagementDto birdManagementDto);
    }
}
