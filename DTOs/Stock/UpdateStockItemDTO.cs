using StockTracking.Models;

namespace StockTracking.DTOs.Stock
{
    public class UpdateStockItemDTO
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int Quantity { get; set; }

        public EStockItemType Type { get; set; }

        public int StockId { get; set; }
    }
}
