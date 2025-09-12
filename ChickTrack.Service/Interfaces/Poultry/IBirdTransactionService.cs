namespace ChickTrack.Service.Interfaces.Poultry
{
    public interface IBirdTransactionService : IMSSQLBaseService<BirdTransaction, long>
    {
        Task<Result<BirdTransactionDto>> CreateAsync(CreateBirdTransactionDto birdTransactionDto);
        Task<Result<bool>> DeleteAsync(long id);
        Task<Result<bool>> UpdateAsync(long id, UpdateBirdTransactionDto birdTransactionDto);
    }
}
