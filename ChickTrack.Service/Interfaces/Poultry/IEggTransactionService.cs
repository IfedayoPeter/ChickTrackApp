using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos;
using ChickTrack.Domain.Entities.Poultry;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces.Poultry
{
    public interface IEggTransactionService : IMSSQLBaseService<EggTransaction, long>
    {
        Task<Result<EggTransactionDto>> CreateAsync(CreateEggTransactionDto eggTransactionDto);
        Task<Result<bool>> DeleteAsync(long id);
        Task<Result<bool>> UpdateAsync(long id, UpdateEggTransactionDto eggTransactionDto);
    }
}
