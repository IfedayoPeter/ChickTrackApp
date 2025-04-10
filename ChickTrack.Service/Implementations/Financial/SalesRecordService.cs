using AutoMapper;
using ChickTrack.Domain.DataTransferObjects.Financial.CreateDtos;
using ChickTrack.Domain.DataTransferObjects.Financial.GetDtos;
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

        public async Task<Result<SaleRecordDto>> CreateSalesRecord(CreateSaleRecordDto saleRecordDto)
        {
            var result = new Result<SaleRecordDto>(false);
            try
            {
                saleRecordDto.Code = RandomGenerator.RandomString(10);

                var saleRecord = _mapper.Map<SaleRecord>(saleRecordDto);
                var response = await _saleRecord.CreateAsync(saleRecord);

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

                    var feedLog = await _feedLog.GetSingleAsync(x => x.FeedBrand == saleRecordDto.FeedBrand);
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
    }
}
