namespace StockTracking.Models
{
    public class Solicitation : BaseEntity
    {
        public ESolicitationStatus Status { get; set; }

        public string RequesterId { get; set; }

        public string? ReviewerId {  get; set; }
        
        public Employee Requester { get; set; }

        public Employee? Reviewer { get; set; }

        public List<SolicitationItem> SolicitationItems { get; set; }
    }
}
