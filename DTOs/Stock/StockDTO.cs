namespace StockTracking.DTOs.Stock;

public class StockDTO
{
    public long Id { get; set; }
    public string Name { get; set; }

    public string? Description { get; set; }

    public List<StockItemEquipmentDTO> StockEquipments { get; set; }

    public List<StockItemMaterialDTO> StockMaterials { get; set; }

}
