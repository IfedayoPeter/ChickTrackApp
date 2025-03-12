using ChickTrack.Domain.Entities.Financials;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces
{
    public interface IInvestmentService : IMSSQLBaseService<Investment, long>
    {
    }
}
