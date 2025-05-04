using ChickTrack.Domain.Entities.Feed;
using ChickTrack.Service.Interfaces.Feed;
using Microsoft.Extensions.Caching.Memory;

namespace ChickTrack.Service.Helpers
{
    public class FeedProfitCalculator
    {
        private readonly IFeedPriceService _feedPriceService;
        private readonly IFeedUnitPriceService _feedUnitPriceService;
        private readonly IFeedSalesUnitService _feedSalesUnitService;
        private readonly IMemoryCache _cache;

        private const string PricesCacheKey = "FeedPricesData";
        private const string UnitPricesCacheKey = "FeedUnitPricesData";
        private const string UnitConversionCacheKey = "FeedUnitConversionData";

        public FeedProfitCalculator(
            IFeedPriceService feedPriceService,
            IFeedUnitPriceService feedUnitPriceService,
            IFeedSalesUnitService feedSalesUnitService,
            IMemoryCache cache)
        {
            _feedPriceService = feedPriceService;
            _feedUnitPriceService = feedUnitPriceService;
            _feedSalesUnitService = feedSalesUnitService;
            _cache = cache;
        }

        public async Task<decimal> CalculateProfit(string feedBrandName, string feedSalesUnitName, int quantity, decimal price)
        {
            // Get or set cache for unit conversions
            var unitConversionResult = await _cache.GetOrCreateAsync(UnitConversionCacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1); // Cache for longer since these rarely change
                return await _feedSalesUnitService.GetAllAsync<FeedSalesUnit>();
            });

            if (!unitConversionResult.IsSuccess || unitConversionResult.Content == null)
            {
                throw new InvalidOperationException("Failed to retrieve unit conversions: " +
                                                 (unitConversionResult.Message ?? "Unknown error"));
            }

            // Create conversion dictionary from service response
            var feedUnitConversion = unitConversionResult.Content
                .ToDictionary(x => x.unitName, x => x.unitQuantity);

            // Rest of your existing code with the dynamic feedUnitConversion
            var feedPricesResult = await _cache.GetOrCreateAsync(PricesCacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return await _feedPriceService.GetActivePricesAsync();
            });

            if (!feedPricesResult.IsSuccess || feedPricesResult.Content == null)
            {
                throw new InvalidOperationException("Failed to retrieve feed prices: " +
                                                 (feedPricesResult.Message ?? "Unknown error"));
            }

            var unitPricesResult = await _cache.GetOrCreateAsync(UnitPricesCacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                return await _feedUnitPriceService.GetActivePricesAsync();
            });

            if (!unitPricesResult.IsSuccess || unitPricesResult.Content == null)
            {
                throw new InvalidOperationException("Failed to retrieve unit prices: " +
                                                 (unitPricesResult.Message ?? "Unknown error"));
            }

            // Find prices
            var bagPrice = feedPricesResult.Content
                .FirstOrDefault(x => x.FeedBrand == feedBrandName)?.PricePerBag;

            var unitPrice = unitPricesResult.Content
                .FirstOrDefault(x => x.FeedBrand == feedBrandName && x.UnitName == feedSalesUnitName)?.Price;

            if (bagPrice == null || unitPrice == null || !feedUnitConversion.ContainsKey(feedSalesUnitName))
            {
                throw new ArgumentException("Invalid feed brand or sales unit.");
            }

            decimal costPricePerUnit = bagPrice.Value * feedUnitConversion[feedSalesUnitName];
            decimal sellingPricePerUnit = price > 0 ? price / quantity : unitPrice.Value;
            var profit = (sellingPricePerUnit - costPricePerUnit) * quantity;

            return profit;
        }
    }
}
