namespace ChickTrack.Service.Interfaces.Feed
{
    public interface IFeedMeasurementPriceService
    {
        Task<FeedMeasurementPricesDto> CalculateAsync(decimal bagPrice, FeedMeasurementCalculationModeEnum calculationMode, decimal profitPercentage = 35m);
    }
}
