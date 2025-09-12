namespace ChickTrack.Service.Interfaces.Poultry
{
    public interface IEggTransactionService : IMSSQLBaseService<EggTransaction, long>
    {
        Task<Result<EggTransactionDto>> CreateAsync(CreateEggTransactionDto eggTransactionDto);
        Task<Result<bool>> DeleteAsync(long id);
        Task<Result<bool>> UpdateAsync(long id, UpdateEggTransactionDto eggTransactionDto);
    }
}
