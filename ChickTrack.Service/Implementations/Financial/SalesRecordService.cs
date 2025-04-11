using AutoMapper;
using ChickTrack.Domain.DataTransferObjects.Financial.CreateDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.UpdateDtos;
using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Domain.Entities.Financials;
using ChickTrack.Service.Helpers;
using ChickTrack.Service.Interfaces.Financial;
using Lagetronix.Rapha.Base.Common.Domain.Common;
using Lagetronix.Rapha.Base.Common.Domain.Utilities;
using Lagetronix.Rapha.Base.Common.Repositories;
using Lagetronix.Rapha.Base.Common.Repositories.Interfaces;
using Lagetronix.Rapha.Base.Common.Services.Implementation;

namespace ChickTrack.Service.Implementations.Financial
{
    public class SalesRecordService : MSSQLBaseService<SaleRecord, long>, ISaleRecordService
    {
        private readonly IMSSQLRepository<SaleRecord, long> _saleRecord;
        private readonly IMSSQLRepository<TotalSales, long> _totalSales;
        private readonly IMSSQLRepository<FeedLog, long> _feedLog;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SalesRecordService(
            IMSSQLRepository<SaleRecord, long> saleRecord,
            IMSSQLRepository<TotalSales, long> totalSales,
            IMSSQLRepository<FeedLog, long> feedLog,
            IApplicationDbContext context,
            IMapper mapper
        ) : base(saleRecord, context, mapper)
        {
            _saleRecord = saleRecord;
            _totalSales = totalSales;
            _feedLog = feedLog;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<SaleRecordDto>> CreateAsync(CreateSaleRecordDto saleRecordDto)
        {
            var result = new Result<SaleRecordDto>(false);
            try
            {
                saleRecordDto.Code = RandomGenerator.RandomString(10);

                var saleRecord = _mapper.Map<SaleRecord>(saleRecordDto);
                var response = await _saleRecord.CreateAsync(saleRecord);
                var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == saleRecordDto.FeedBrand);
                if (feedLog == null || feedLog.AvailableBags <= 0)
                {
                    result.SetError($"{typeof(SaleRecord).Name} not recorded", $"{saleRecordDto.FeedBrand.ToString()} is currently unavailable");
                    return result;
                }

                await _context.SaveChangesAsync();
                var existingSalesRecord = await _saleRecord.GetSingleWithIncludeAsync(x => x.Code == saleRecordDto.Code, x => x.FeedSalesUnit);

                if (saleRecordDto.SalesType == Domain.Enums.SalesTypeEnum.Feed)
                {
                    var totalSales = await _totalSales.GetSingleAsync(x => x.FeedBrand == saleRecordDto.FeedBrand);
                    if (totalSales == null)
                    {
                        totalSales = new TotalSales
                        {
                            Code = RandomGenerator.RandomString(10),
                            FeedBrand = saleRecordDto.FeedBrand.HasValue ? saleRecordDto.FeedBrand.Value : throw new ArgumentNullException(nameof(saleRecordDto.FeedBrand)),
                            BagsSold = existingSalesRecord.FeedSalesUnit.unitQuantity,
                            Amount = saleRecordDto.Price,
                            Profit = FeedProfitCalculator.CalculateProfit(saleRecordDto.FeedBrand.ToString(), existingSalesRecord.FeedSalesUnit.unitName, saleRecord.Quantity, saleRecord.Price)
                        };
                        await _totalSales.CreateAsync(totalSales);
                    }
                    else
                    {
                        totalSales.BagsSold += existingSalesRecord.FeedSalesUnit.unitQuantity;
                        totalSales.Amount += saleRecordDto.Price;
                        totalSales.Profit += FeedProfitCalculator.CalculateProfit(saleRecordDto.FeedBrand.ToString(), existingSalesRecord.FeedSalesUnit.unitName, saleRecordDto.Quantity, saleRecordDto.Price);
                        await _totalSales.UpdateAsync(totalSales.Id, totalSales);
                    }

                    
                    if (feedLog != null)
                    {
                        feedLog.BagsSold += existingSalesRecord.FeedSalesUnit.unitQuantity;
                        feedLog.AvailableBags -= existingSalesRecord.FeedSalesUnit.unitQuantity;
                        await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                    }
                    else
                    {
                        feedLog = new FeedLog
                        {
                            Code = RandomGenerator.RandomString(10),
                            FeedBrand = saleRecordDto.FeedBrand.HasValue ? saleRecordDto.FeedBrand.Value : throw new ArgumentNullException(nameof(saleRecordDto.FeedBrand)),
                            BagsSold = existingSalesRecord.FeedSalesUnit.unitQuantity
                        };
                        await _feedLog.CreateAsync(feedLog);
                    }
                }

                await _context.SaveChangesAsync();

                if (response == null)
                {
                    result.SetError($"{typeof(SaleRecord).Name} not created", $"{typeof(SaleRecord).Name} not created");
                }
                else
                {
                    result.SetSuccess(_mapper.Map<SaleRecordDto>(response), $"{typeof(SaleRecord).Name} created successfully!");
                }
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), $"Error while creating {typeof(SaleRecord).Name}");
            }
            return result;
        }

        public async Task<Result<SaleRecordDto>> UpdateAsync(long id, UpdateSalesRecordDto saleRecordDto)
        {
            var result = new Result<SaleRecordDto>(false);
            try
            {
                var existingSale = await _saleRecord.GetSingleWithIncludeAsync(x => x.Id == id, x => x.FeedSalesUnit);
                if (existingSale == null)
                {
                    result.SetError("Sale not found", "Sale record not found.");
                    return result;
                }

                // Revert the original values from related entities
                if (existingSale.SalesType == Domain.Enums.SalesTypeEnum.Feed)
                {
                    var totalSales = await _totalSales.GetSingleAsync(x => x.FeedBrand == existingSale.FeedBrand);
                    var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == existingSale.FeedBrand);

                    if (totalSales != null)
                    {
                        totalSales.BagsSold -= existingSale.FeedSalesUnit.unitQuantity;
                        totalSales.Amount -= existingSale.Price;
                        totalSales.Profit -= FeedProfitCalculator.CalculateProfit(existingSale.FeedBrand.ToString(), existingSale.FeedSalesUnit.unitName, existingSale.Quantity, existingSale.Price);
                        await _totalSales.UpdateAsync(totalSales.Id, totalSales);
                    }

                    if (feedLog != null)
                    {
                        feedLog.BagsSold -= existingSale.FeedSalesUnit.unitQuantity;
                        feedLog.AvailableBags += existingSale.FeedSalesUnit.unitQuantity;
                        await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                    }
                }

                // Map and apply the updated values
                _mapper.Map(saleRecordDto, existingSale);
                await _saleRecord.UpdateAsync(id, existingSale);
                await _context.SaveChangesAsync();

                // Apply updated values to related entities
                var updatedRecord = await _saleRecord.GetSingleWithIncludeAsync(x => x.Id == id, x => x.FeedSalesUnit);
                if (saleRecordDto.SalesType == Domain.Enums.SalesTypeEnum.Feed)
                {
                    var totalSales = await _totalSales.GetSingleAsync(x => x.FeedBrand == saleRecordDto.FeedBrand);
                    if (totalSales != null)
                    {
                        totalSales.BagsSold += updatedRecord.FeedSalesUnit.unitQuantity;
                        totalSales.Amount += saleRecordDto.Price;
                        totalSales.Profit += FeedProfitCalculator.CalculateProfit(saleRecordDto.FeedBrand.ToString(), updatedRecord.FeedSalesUnit.unitName, saleRecordDto.Quantity, saleRecordDto.Price);
                        await _totalSales.UpdateAsync(totalSales.Id, totalSales);
                    }

                    var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == saleRecordDto.FeedBrand);
                    if (feedLog != null)
                    {
                        feedLog.BagsSold += updatedRecord.FeedSalesUnit.unitQuantity;
                        feedLog.AvailableBags -= updatedRecord.FeedSalesUnit.unitQuantity;
                        await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                    }
                }

                result.SetSuccess(_mapper.Map<SaleRecordDto>(existingSale), "Sale record updated successfully!");
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while updating sale record.");
            }
            return result;
        }

        public async Task<Result<bool>> DeleteAsync(long id)
        {
            var result = new Result<bool>(false);
            try
            {
                var sale = await _saleRecord.GetSingleWithIncludeAsync(x => x.Id == id, x => x.FeedSalesUnit);
                if (sale == null)
                {
                    result.SetError("Sale record not found", "Sale not found.");
                    return result;
                }

                if (sale.SalesType == Domain.Enums.SalesTypeEnum.Feed)
                {
                    var totalSales = await _totalSales.GetSingleAsync(x => x.FeedBrand == sale.FeedBrand);
                    var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == sale.FeedBrand);

                    if (totalSales != null)
                    {
                        totalSales.BagsSold -= sale.FeedSalesUnit.unitQuantity;
                        totalSales.Amount -= sale.Price;
                        totalSales.Profit -= FeedProfitCalculator.CalculateProfit(sale.FeedBrand.ToString(), sale.FeedSalesUnit.unitName, sale.Quantity, sale.Price);
                        await _totalSales.UpdateAsync(totalSales.Id, totalSales);
                    }

                    if (feedLog != null)
                    {
                        feedLog.BagsSold -= sale.FeedSalesUnit.unitQuantity;
                        feedLog.AvailableBags += sale.FeedSalesUnit.unitQuantity;
                        await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                    }
                }

                await _saleRecord.DeleteAsync(id);
                await _context.SaveChangesAsync();

                result.SetSuccess(true, "Sale record deleted successfully!");
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while deleting sale record.");
            }
            return result;
        }


    }
}
