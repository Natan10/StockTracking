namespace StockTracking.Models
{
    public class Solicitation : BaseEntity
    {
        public int Id { get; set; }

        public ESolicitationStatus Status { get; set; }

        public string RequesterId { get; set; }

        public Employee Requester { get; set; }

        public string? ReviewerId {  get; set; }
        
        public Employee? Reviewer { get; set; }

        public List<SolicitationItem> SolicitationItems { get; set; }
    }
}
