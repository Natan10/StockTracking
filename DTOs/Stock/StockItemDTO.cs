using StockTracking.Models;

namespace StockTracking.DTOs.Stock
{
    public class StockItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Quantity { get; set; }

        public EStockItemType Type { get; set; }

        public int StockId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
