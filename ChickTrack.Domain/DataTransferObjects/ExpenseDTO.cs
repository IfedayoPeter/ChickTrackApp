namespace ChickTrack.Domain.DataTransferObjects
{
    public class ExpenseDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
