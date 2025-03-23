using ChickTrack.Domain.Entities.Financials;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces.Financial
{
    public interface ITotalSalesService : IMSSQLBaseService<TotalSales, long>
    {
    }
}
