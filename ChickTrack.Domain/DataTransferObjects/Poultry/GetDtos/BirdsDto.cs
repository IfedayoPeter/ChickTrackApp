namespace ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos
{
    public class BirdsDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string UserId { get; set; }
        public int MaleBirds { get; set; }
        public int FemaleBirds { get; set; }
        public int Chicks { get; set; }
        public int BirdsSold { get; set; }
        public int BirdsLost { get; set; }
        public int TotalAvailableBirds { get; set; }
        public int TotalBirds { get; set; }
    }
}
