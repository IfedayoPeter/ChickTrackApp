using ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos;
using ChickTrack.Domain.DataTransferObjects.Poultry;
using ChickTrack.Domain.Entities.Poultry;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Services.Interface;

namespace ChickTrack.Service.Interfaces.Poultry
{
    public interface IBirdTransactionService : IMSSQLBaseService<BirdTransaction, long>
    {
        Task<Result<BirdTransactionDto>> CreateAsync(CreateBirdTransactionDto birdTransactionDto);
        Task<Result<bool>> DeleteAsync(long id);
        Task<Result<bool>> UpdateAsync(long id, UpdateBirdTransactionDto birdTransactionDto);
    }
}
