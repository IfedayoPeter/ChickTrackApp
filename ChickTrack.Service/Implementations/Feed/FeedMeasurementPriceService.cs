namespace ChickTrack.Service.Implementations.Feed
{
    public class FeedMeasurementPriceService : IFeedMeasurementPriceService
    {
        private readonly IFeedSalesUnitService _feedSalesUnitService;

        private const decimal ReferenceBagPrice = 15200m;
        private const decimal PaintReferencePrice = 2565m;
        private const decimal HalfPaintReferencePrice = 1283m;
        private const decimal DericaReferencePrice = 513m;
        private const decimal HalfDericaReferencePrice = 257m;
        private const decimal CupReferencePrice = 128m;
        private const decimal PaintQuantity = 0.125m;
        private const decimal HalfPaintQuantity = 0.0625m;
        private const decimal DericaQuantity = 0.025m;
        private const decimal HalfDericaQuantity = 0.0125m;
        private const decimal CupQuantity = 0.00625m;

        public FeedMeasurementPriceService(IFeedSalesUnitService feedSalesUnitService)
        {
            _feedSalesUnitService = feedSalesUnitService;
        }

        public async Task<FeedMeasurementPricesDto> CalculateAsync(decimal bagPrice, FeedMeasurementCalculationModeEnum calculationMode, decimal profitPercentage = 35m)
        {
            if (bagPrice <= 0)
            {
                throw new ArgumentException("Bag price must be greater than 0.", nameof(bagPrice));
            }
            if (profitPercentage < 0)
            {
                throw new ArgumentException("Profit percentage cannot be less than 0.", nameof(profitPercentage));
            }

            if (calculationMode == FeedMeasurementCalculationModeEnum.UseReferencePrices)
            {
                return CalculateFromReferencePrices(bagPrice, calculationMode);
            }

            return await CalculateFromFeedSalesUnitOrFallbackAsync(bagPrice, calculationMode, profitPercentage);
        }

        private async Task<FeedMeasurementPricesDto> CalculateFromFeedSalesUnitOrFallbackAsync(decimal bagPrice, FeedMeasurementCalculationModeEnum requestedMode, decimal profitPercentage)
        {
            var feedSalesUnitsResult = await _feedSalesUnitService.GetAllAsync<FeedSalesUnitDto>();
            var feedSalesUnits = feedSalesUnitsResult?.Content;

            if (feedSalesUnits == null || feedSalesUnits.Count == 0)
            {
                var fallbackResponse = CalculateFromReferencePrices(bagPrice, requestedMode);
                fallbackResponse.UsedFallback = true;
                fallbackResponse.Notification = "FeedSalesUnit data is null or empty. Reference prices were used.";
                return fallbackResponse;
            }

            var sellingBagPrice = bagPrice * (1 + (profitPercentage / 100m));
            return new FeedMeasurementPricesDto
            {
                RequestedCalculationMode = requestedMode,
                AppliedCalculationMode = FeedMeasurementCalculationModeEnum.UseFeedSalesUnit,
                ProfitPercentageUsed = profitPercentage,
                BagPrice = bagPrice,
                Paint = ScaleByQuantity(GetUnitQuantity(feedSalesUnits, "Paint"), sellingBagPrice),
                HalfPaint = ScaleByQuantity(GetUnitQuantity(feedSalesUnits, "HalfPaint"), sellingBagPrice),
                Derica = ScaleByQuantity(GetUnitQuantity(feedSalesUnits, "Derica"), sellingBagPrice),
                HalfDerica = ScaleByQuantity(GetUnitQuantity(feedSalesUnits, "HalfDerica"), sellingBagPrice),
                Cup = ScaleByQuantity(GetUnitQuantity(feedSalesUnits, "Cup"), sellingBagPrice)
            };
        }

        private static FeedMeasurementPricesDto CalculateFromReferencePrices(decimal bagPrice, FeedMeasurementCalculationModeEnum requestedMode)
        {
            return new FeedMeasurementPricesDto
            {
                RequestedCalculationMode = requestedMode,
                AppliedCalculationMode = FeedMeasurementCalculationModeEnum.UseReferencePrices,
                ProfitPercentageUsed = CalculateReferenceProfitPercentage(),
                BagPrice = bagPrice,
                Paint = ScaleReferencePrice(PaintReferencePrice, bagPrice),
                HalfPaint = ScaleReferencePrice(HalfPaintReferencePrice, bagPrice),
                Derica = ScaleReferencePrice(DericaReferencePrice, bagPrice),
                HalfDerica = ScaleReferencePrice(HalfDericaReferencePrice, bagPrice),
                Cup = ScaleReferencePrice(CupReferencePrice, bagPrice)
            };
        }

        private static decimal GetUnitQuantity(IList<FeedSalesUnitDto> feedSalesUnits, string unitName)
        {
            var unit = feedSalesUnits.FirstOrDefault(x => x.UnitName.Equals(unitName, StringComparison.OrdinalIgnoreCase));
            return unit?.UnitQuantity ?? 0m;
        }

        private static decimal ScaleReferencePrice(decimal referenceMeasurementPrice, decimal bagPrice)
        {
            return Math.Round((referenceMeasurementPrice / ReferenceBagPrice) * bagPrice, 0, MidpointRounding.AwayFromZero);
        }

        private static decimal ScaleByQuantity(decimal unitQuantity, decimal bagPrice)
        {
            return Math.Round(unitQuantity * bagPrice, 0, MidpointRounding.AwayFromZero);
        }

        private static decimal CalculateReferenceProfitPercentage()
        {
            var impliedBagPriceFromPaint = PaintReferencePrice / PaintQuantity;
            var impliedBagPriceFromHalfPaint = HalfPaintReferencePrice / HalfPaintQuantity;
            var impliedBagPriceFromDerica = DericaReferencePrice / DericaQuantity;
            var impliedBagPriceFromHalfDerica = HalfDericaReferencePrice / HalfDericaQuantity;
            var impliedBagPriceFromCup = CupReferencePrice / CupQuantity;

            var averageImpliedBagPrice = (impliedBagPriceFromPaint + impliedBagPriceFromHalfPaint + impliedBagPriceFromDerica + impliedBagPriceFromHalfDerica + impliedBagPriceFromCup) / 5m;
            return Math.Round(((averageImpliedBagPrice - ReferenceBagPrice) / ReferenceBagPrice) * 100m, 2, MidpointRounding.AwayFromZero);
        }
    }
}
