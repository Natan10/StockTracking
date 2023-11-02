using StockTracking.Models;

namespace StockTracking.DTOs.Stock
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public List<StockItemDTO> StockItems { get; set; }
    }
}
