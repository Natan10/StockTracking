using StockTracking.Models;
using System.ComponentModel.DataAnnotations;

namespace StockTracking.DTOs.Stock
{
    public class CreateStockItemDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public int Quantity { get; set; }

        public EStockItemType Type { get; set; } = EStockItemType.MATERIAL;

        [Required]
        public int StockId { get; set; }
    }
}
