using StockTracking.DTOs.Auth;
using StockTracking.Models;

namespace StockTracking.DTOs.Solicitation
{
    public class SolicitationDTO
    {
        public int Id { get; set; }

        public ESolicitationStatus Status { get; set; }

        public string RequesterId { get; set; }

        public EmployeeDTO Requester { get; set; }

        public string? ReviewerId { get; set; }

        public EmployeeDTO? Reviewer { get; set; }

        public long StockId {  get; set; }

        public List<SolicitationItemDTO> SolicitationItems { get; set; }

        public DateTime? CreatedAt { get; set; }
        
        public DateTime? UpdatedAt { get; set; }
    }
}
