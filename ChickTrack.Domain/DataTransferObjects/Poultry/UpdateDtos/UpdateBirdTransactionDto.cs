using ChickTrack.Domain.Enums;

namespace ChickTrack.Domain.DataTransferObjects.Poultry
{
    public class UpdateBirdTransactionDto : CreateBirdTransactionDto
    {
        public long Id { get; set; }
       
    }
}
