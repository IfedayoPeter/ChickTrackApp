using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Feed
{
    public class FeedInventoryDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public FeedBrandEnum FeedBrand { get; set; }
        public decimal BagsBought { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
