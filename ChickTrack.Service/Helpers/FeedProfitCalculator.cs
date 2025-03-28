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
        { "Cup", 0.0078125m },
        { "Half Derica", 0.015625m },
        { "Derica", 0.03125m },
        { "Half Paint", 0.0625m },
        { "Paint", 0.125m },
        { "Bag", 1.0m }
    };

        private static readonly Dictionary<string, Dictionary<string, decimal>> FeedUnitPrices = new Dictionary<string, Dictionary<string, decimal>>
    {
        { "NewHopeStarter", new Dictionary<string, decimal> { { "Cup", 250 }, { "Half Derica", 500 }, { "Derica", 950 }, { "Half Paint", 1850 }, { "Paint", 3700 } } },
        { "NewHopeLayer", new Dictionary<string, decimal> { { "Cup", 200 }, { "Half Derica", 400 }, { "Derica", 750 }, { "Half Paint", 1500 }, { "Paint", 3000 } } },
        { "TopFeedGrower", new Dictionary<string, decimal> { { "Cup", 200 }, { "Half Derica", 400 }, { "Derica", 800 }, { "Half Paint", 1600 }, { "Paint", 3200 } } },
        { "TopFeedFinisher", new Dictionary<string, decimal> { { "Cup", 250 }, { "Half Derica", 450 }, { "Derica", 900 }, { "Half Paint", 1750 }, { "Paint", 3500 } } }
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

            decimal sellingPricePerUnit = price > 0 ? price / quantity : normalSellingPricePerUnit;
            decimal profitPerUnit = sellingPricePerUnit - costPricePerUnit;

            var profit = profitPerUnit * quantity;
            return profit;
        }
    }
}
