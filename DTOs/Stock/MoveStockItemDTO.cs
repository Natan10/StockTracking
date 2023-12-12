using StockTracking.Models;

namespace StockTracking.DTOs.Stock;

public class MoveStockItemDTO
{

    public long FromStockId { get; set; }

    public long ToStockId { get; set; }

    public string Identifier { get; set; }

    public EStockItemType Type { get; set; }

    public int Quantity { get; set; }
}
