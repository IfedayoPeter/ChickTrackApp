using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Feed
{
    public class FeedLogDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public FeedBrandEnum FeedBrand { get; set; }
        public int BagsBought { get; set; }
        public int BagsSold { get; set; }
        public int AvailableBags { get; set; }
    }
}
