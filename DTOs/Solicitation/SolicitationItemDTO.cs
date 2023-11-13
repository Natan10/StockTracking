using StockTracking.Models;

namespace StockTracking.DTOs.Solicitation
{
    public class SolicitationItemDTO
    {
        public int ProcessedQuantity { get; set; }

        public int RequiredQuantity { get; set; }

        public int StockItemId { get; set; }
    }
}
