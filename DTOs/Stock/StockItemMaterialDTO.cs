namespace StockTracking.DTOs.Stock;

public class StockItemMaterialDTO
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public string MeasurementUnit { get; set; }

    public int Quantity { get; set; }

    public long StockId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
