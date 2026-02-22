namespace Domain.DataTransferObjects.Feed
{
    public class FeedMeasurementPricesDto
    {
        public FeedMeasurementCalculationModeEnum RequestedCalculationMode { get; set; }
        public FeedMeasurementCalculationModeEnum AppliedCalculationMode { get; set; }
        public decimal ProfitPercentageUsed { get; set; }
        public bool UsedFallback { get; set; }
        public string? Notification { get; set; }
        public decimal BagPrice { get; set; }
        public decimal Paint { get; set; }
        public decimal HalfPaint { get; set; }
        public decimal Derica { get; set; }
        public decimal HalfDerica { get; set; }
        public decimal Cup { get; set; }
    }
}
