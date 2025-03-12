namespace ChickTrack.Domain.DataTransferObjects
{
    public class PoultryDTO
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public int MaleBirds { get; set; }
        public int FemaleBirds { get; set; }
        public int BirdsSold { get; set; }
        public int BirdsLost { get; set; }

        public int TotalAvailableBirds => MaleBirds + FemaleBirds - BirdsSold - BirdsLost;
        public int TotalBirds => MaleBirds + FemaleBirds + BirdsSold + BirdsLost;
    }

}
