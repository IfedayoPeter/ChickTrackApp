using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Feed
{
    public class FeedLogDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public FeedBrandEnum FeedBrand { get; set; }
        public string FeedBrandName { get { return FeedBrand.ToString(); } }
        public decimal BagsBought { get; set; }
        public decimal BagsSold { get; set; }
        public decimal AvailableBags { get; set; }
    }
}
