using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class CreateBirdTransactionDto
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int BirdsLost { get; set; }
        public int BirdsSold { get; set; }
        public GenderEnum Gender { get; set; } // Male, Female, Chicks
        public decimal Amount { get; set; }
    }
}
