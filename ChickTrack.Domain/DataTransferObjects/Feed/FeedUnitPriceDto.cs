using Domain.Enums;

namespace Domain.DataTransferObjects.Feed
{
    public class FeedUnitPriceDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public FeedBrandEnum FeedBrandEnum { get; set; }
        public string FeedBrand { get { return FeedBrandEnum.ToString(); } }
        public FeedUnitEnum FeedUnitEnum { get; set; }
        public string UnitName { get { return FeedUnitEnum.ToString(); } }
        public decimal Price { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
