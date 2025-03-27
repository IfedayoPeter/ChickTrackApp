namespace ChickTrack.Service.Helpers
{
    public class FeedProfitCalculator
    {
        private static readonly Dictionary<string, decimal> FeedPricesPerBag = new Dictionary<string, decimal>
    {
        { "NewHopeStarter", 27700 },
        { "NewHopeLayer", 19900 },
        { "TopFeedGrower", 19600 },
        { "TopFeedFinisher", 21800 }
    };

        private static readonly Dictionary<string, decimal> FeedUnitConversion = new Dictionary<string, decimal>
    {
        { "cup", 0.0078125m },
        { "half derica", 0.015625m },
        { "derica", 0.03125m },
        { "half paint", 0.0625m },
        { "paint", 0.125m },
        { "bag", 1.0m }
    };

        private static readonly Dictionary<string, Dictionary<string, decimal>> FeedUnitPrices = new Dictionary<string, Dictionary<string, decimal>>
    {
        { "NewHopeStarter", new Dictionary<string, decimal> { { "cup", 250 }, { "half derica", 500 }, { "derica", 950 }, { "half paint", 1850 }, { "paint", 3700 } } },
        { "NewHopeLayer", new Dictionary<string, decimal> { { "cup", 200 }, { "half derica", 400 }, { "derica", 750 }, { "half paint", 1500 }, { "paint", 3000 } } },
        { "TopFeedGrower", new Dictionary<string, decimal> { { "cup", 200 }, { "half derica", 400 }, { "derica", 800 }, { "half paint", 1600 }, { "paint", 3200 } } },
        { "TopFeedFinisher", new Dictionary<string, decimal> { { "cup", 250 }, { "half derica", 450 }, { "derica", 900 }, { "half paint", 1750 }, { "paint", 3500 } } }
    };

        public static decimal CalculateProfit(string feedBrandName, string feedSalesUnitName, int quantity, decimal price)
        {
            if (!FeedPricesPerBag.ContainsKey(feedBrandName) || !FeedUnitConversion.ContainsKey(feedSalesUnitName) ||
                !FeedUnitPrices.ContainsKey(feedBrandName) || !FeedUnitPrices[feedBrandName].ContainsKey(feedSalesUnitName))
            {
                throw new ArgumentException("Invalid feed brand or sales unit.");
            }

            decimal bagPrice = FeedPricesPerBag[feedBrandName];
            decimal unitFraction = FeedUnitConversion[feedSalesUnitName];
            decimal costPricePerUnit = bagPrice * unitFraction;
            decimal normalSellingPricePerUnit = FeedUnitPrices[feedBrandName][feedSalesUnitName];

            decimal sellingPricePerUnit = price > 0 ? price : normalSellingPricePerUnit;
            decimal profitPerUnit = sellingPricePerUnit - costPricePerUnit;

            var profit = profitPerUnit * quantity;
            return profit;
        }
    }
}
