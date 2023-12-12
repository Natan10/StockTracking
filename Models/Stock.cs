namespace StockTracking.Models
{
    public class Stock : BaseEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public virtual List<StockItemMaterial> StockItemMaterials { get; set; }

        public virtual List<StockItemEquipment> StockItemEquipments { get; set; }

        
        public StockItemMaterial? GetStockItemMaterialByCode(string code)
        {
            return StockItemMaterials.FirstOrDefault(x => x.Code == code);
        }

        public StockItemEquipment? GetStockItemEquipmentBySerial(string serial)
        {
            return StockItemEquipments.FirstOrDefault(x => x.Serial == serial);
        }

    }
}
