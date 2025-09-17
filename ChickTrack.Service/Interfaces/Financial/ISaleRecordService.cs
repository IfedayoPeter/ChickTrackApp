namespace ChickTrack.Service.Interfaces.Financial
{
    public interface ISaleRecordService : IMSSQLBaseService<SaleRecord, long>
    {
        Task<Result<SaleRecordDto>> CreateAsync(CreateSaleRecordDto saleRecordDto);
        Task<Result<SaleRecordDto>> UpdateAsync(long id, UpdateSalesRecordDto saleRecordDto);
        Task<Result<bool>> DeleteAsync(long id);
        Task<Result<List<long>>> ImportAsync(CreateSaleRecordDto[] saleRecordDtos);
    }
}
