using ChickTrack.Domain.Entities.Poultry;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces.Poultry
{
    public interface IBirdService : IMSSQLBaseService<Birds, long>
    {
    }
}
