using ChickTrack.Domain.Entities.Poultry;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces
{
    public interface IPoultryService : IMSSQLBaseService<Birds, long>
    {
    }
}
