

namespace ChickTrack.Service.Implementations.Financial
{
    public class SalesRecordService : MSSQLBaseService<SaleRecord, long>, ISaleRecordService
    {
        private readonly IMSSQLRepository<SaleRecord, long> _saleRecord;
        private readonly IMSSQLRepository<TotalSales, long> _totalSales;
        private readonly IMSSQLRepository<FeedLog, long> _feedLog;
        private readonly IMSSQLRepository<FeedSalesUnit, long> _feedSalesUnit;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly FeedProfitCalculator _feedProfitCalculator;
        public SalesRecordService(
            IMSSQLRepository<SaleRecord, long> saleRecord,
            IMSSQLRepository<TotalSales, long> totalSales,
            IMSSQLRepository<FeedLog, long> feedLog,
            IMSSQLRepository<FeedSalesUnit, long> feedSalesUnit,
            IApplicationDbContext context,
            IMapper mapper,
            FeedProfitCalculator feedProfitCalculator
        ) : base(saleRecord, context, mapper)
        {
            _saleRecord = saleRecord;
            _totalSales = totalSales;
            _feedLog = feedLog;
            _feedSalesUnit = feedSalesUnit;
            _context = context;
            _mapper = mapper;
            _feedProfitCalculator = feedProfitCalculator;
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
                            Profit = (await _feedProfitCalculator.CalculateProfit(saleRecordDto.FeedBrand.ToString() ?? string.Empty, existingSalesRecord.FeedSalesUnit.unitName, saleRecord.Quantity, saleRecord.Price)).Content
                        };
                        await _totalSales.CreateAsync(totalSales);
                    }
                    else
                    {
                        totalSales.BagsSold += existingSalesRecord.FeedSalesUnit.unitQuantity;
                        totalSales.Amount += saleRecordDto.Price;
                        totalSales.Profit += (await _feedProfitCalculator.CalculateProfit(saleRecordDto.FeedBrand.ToString() ?? string.Empty, existingSalesRecord.FeedSalesUnit.unitName, saleRecordDto.Quantity, saleRecordDto.Price)).Content;
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
                        totalSales.Profit -= (await _feedProfitCalculator.CalculateProfit(existingSale.FeedBrand.ToString(), existingSale.FeedSalesUnit.unitName, existingSale.Quantity, existingSale.Price)).Content;
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
                        totalSales.Profit += (await _feedProfitCalculator.CalculateProfit(saleRecordDto.FeedBrand.ToString(), updatedRecord.FeedSalesUnit.unitName, saleRecordDto.Quantity, saleRecordDto.Price)).Content;
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
                await _context.SaveChangesAsync();

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
                        totalSales.Profit -= (await _feedProfitCalculator.CalculateProfit(sale.FeedBrand.ToString(), sale.FeedSalesUnit.unitName, sale.Quantity, sale.Price)).Content;
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


        public async Task<Result<List<long>>> ImportAsync(CreateSaleRecordDto[] saleRecordDtos)
        {
            var result = new Result<List<long>>(false);
            var importedIds = new List<long>();

            try
            {
                if (saleRecordDtos == null || saleRecordDtos.Length == 0)
                {
                    result.SetError("No data to import", "The import list is empty.");
                    return result;
                }

                // Group sales by FeedBrand for batch processing
                var salesByBrand = saleRecordDtos
                    .Where(dto => dto.FeedBrand != null)
                    .GroupBy(dto => dto.FeedBrand.Value);

                // Get all existing TotalSales records in one query
                var brandIds = salesByBrand.Select(g => g.Key).ToList();
                var existingTotalSales = (await _totalSales.GetAllAsync(x => brandIds.Contains(x.FeedBrand)))
                    .ToDictionary(ts => ts.FeedBrand);

                // Process each brand group
                foreach (var brandGroup in salesByBrand)
                {
                    var feedBrand = brandGroup.Key;
                    var brandSales = brandGroup.ToList();

                    // Get or create TotalSales record (only once per brand)
                    var totalSales = existingTotalSales.TryGetValue(feedBrand, out var ts)
                        ? ts
                        : new TotalSales
                        {
                            Code = RandomGenerator.RandomString(10),
                            FeedBrand = feedBrand,
                            BagsSold = 0,
                            Amount = 0,
                            Profit = 0
                        };

                    bool isNewTotalSales = totalSales.Id == 0;

                    // Process each sale in this brand group
                    foreach (var dto in brandSales)
                    {
                        dto.Code = RandomGenerator.RandomString(10);

                        var saleRecord = _mapper.Map<SaleRecord>(dto);
                        var created = await _saleRecord.CreateAsync(saleRecord);
                        if (created == null)
                            continue;

                        importedIds.Add(created.Id);

                        if (dto.SalesType == Domain.Enums.SalesTypeEnum.Feed)
                        {
                            var feedSalesUnit = dto.FeedSalesUnitId.HasValue
                                ? await _feedSalesUnit.GetByIdAsync(dto.FeedSalesUnitId.Value)
                                : null;

                            if (dto.FeedSalesUnitId.HasValue && feedSalesUnit == null)
                            {
                                result.SetError("Invalid Feed Sales Unit", $"FeedSalesUnitId {dto.FeedSalesUnitId} is invalid");
                                return result;
                            }

                            var unitQuantity = feedSalesUnit?.unitQuantity * dto.Quantity ?? 0;

                            // Update TotalSales
                            totalSales.BagsSold += unitQuantity;
                            totalSales.Amount += dto.Price;
                            totalSales.Profit += (await _feedProfitCalculator.CalculateProfit(
                                dto.FeedBrand.ToString(),
                                feedSalesUnit?.unitName,
                                dto.Quantity,
                                dto.Price)).Content;

                            // Update FeedLog
                            var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == feedBrand);
                            if (feedLog == null)
                            {
                                feedLog = new FeedLog
                                {
                                    Code = RandomGenerator.RandomString(10),
                                    FeedBrand = feedBrand,
                                    BagsSold = unitQuantity,
                                    BagsBought = 0,
                                    AvailableBags = 0 - unitQuantity
                                };
                                await _feedLog.CreateAsync(feedLog);
                            }
                            else
                            {
                                feedLog.BagsSold += unitQuantity;
                                feedLog.AvailableBags = feedLog.BagsBought - feedLog.BagsSold;
                                await _feedLog.UpdateAsync(feedLog.Id, feedLog);
                            }
                        }
                    }

                    // Save TotalSales (create or update)
                    if (isNewTotalSales)
                    {
                        await _totalSales.CreateAsync(totalSales);
                        existingTotalSales[feedBrand] = totalSales; // Add to dictionary for reference
                    }
                    else
                    {
                        await _totalSales.UpdateAsync(totalSales.Id, totalSales);
                    }
                }

                await _context.SaveChangesAsync();
                result.SetSuccess(importedIds, "Sales records imported successfully.");
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString(), "Error while importing Sale Records");
            }

            return result;
        }



    }
}
