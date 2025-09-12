

namespace Domain.DataTransferObjects.Feed
{
    public class FeedPriceDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public FeedBrandEnum FeedBrandEnum { get; set; }
        public string FeedBrand { get { return FeedBrandEnum.ToString(); } }
        public decimal PricePerBag { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsActive { get; set; }
    }
}
