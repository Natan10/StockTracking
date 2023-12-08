namespace StockTracking.DTOs.Stock
{
    public class UpdateStockItemMaterialDTO
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string MeasurementUnit { get; set; }

        public int Quantity { get; set; }

    }
}
