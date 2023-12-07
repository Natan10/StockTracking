namespace StockTracking.Models
{
    public class StockItemMaterial : StockItem
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string MeasurementUnit { get; set; }
    }
}
