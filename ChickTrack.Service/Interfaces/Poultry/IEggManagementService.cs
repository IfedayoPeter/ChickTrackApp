using ChickTrack.Domain.Entities.Poultry;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces.Poultry
{
    public interface IEggManagementService : IMSSQLBaseService<EggManagement, long>
    {
    }
}
