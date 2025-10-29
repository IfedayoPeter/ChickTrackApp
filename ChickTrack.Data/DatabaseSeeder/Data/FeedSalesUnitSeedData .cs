namespace ChickTrack.Data.DatabaseSeeder.Data
{
    internal class FeedSalesUnitSeedData
    {
        public static List<FeedSalesUnit> GetFeedSalesUnits()
        {
            return new List<FeedSalesUnit>
                {
                    new FeedSalesUnit
                    {
                        unitName = "Bag",
                        unitQuantity = 1
                    },
                    new FeedSalesUnit
                    {
                        unitName = "Paint",
                        unitQuantity = 0.1250M
                    },
                    new FeedSalesUnit
                    {
                        unitName = "HalfPaint",
                        unitQuantity = .06250M
                    },
                    new FeedSalesUnit
                    {
                        unitName = "Derica",
                        unitQuantity = .0250M
                    },
                    new FeedSalesUnit
                    {
                        unitName = "HalfDerica",
                        unitQuantity = .01250M
                    },
                    new FeedSalesUnit
                    {
                        unitName = "Cup",
                        unitQuantity = .006250M
                    },

                };
        }
    }
}
