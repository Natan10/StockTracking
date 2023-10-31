namespace StockTracking.Models
{
    public class StockItem : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int Quantity { get; set; }

        public EStockItemType Type { get; set; } = EStockItemType.MATERIAL;

        public int StockId { get; set; }

        public Stock Stock { get; set; }
    }
}
