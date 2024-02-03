using StockTracking.Models;

namespace StockTracking.DTOs.Solicitation
{
    public class CreateSolicitationDTO
    {
        public ESolicitationStatus Status { get; set; }

        public string RequesterId { get; set; }

        public string? ReviewerId { get; set; }

        public long StockId {  get; set; }

        public List<SolicitationItemDTO> SolicitationItems { get; set; }
    }
}
