using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry.GetDtos
{
    public class BirdTransactionDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public int BirdsLost { get; set; }
        public int BirdsSold { get; set; }
        public GenderEnum Gender { get; set; } // Male, Female, Chicks
        public decimal Amount { get; set; }
    }
}
