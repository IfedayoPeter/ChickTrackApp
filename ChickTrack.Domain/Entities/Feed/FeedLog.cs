using ChickTrack.Domain.Enums;
using Lagetronix.Rapha.Base.Common.Domain.Entities;

namespace ChickTrack.Domain.Entities.Feed
{
    public class FeedLog : BaseEntity<long>
    {
        public FeedBrandEnum FeedBrand { get; set; }
        public int BagsBought { get; set; }
        public int BagsSold { get; set; }
        public int AvailableBags { get; set; }
    }
}
