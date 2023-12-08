namespace StockTracking.DTOs.Stock
{
    public class StockItemEquipmentDTO
    {
        public long Id { get; set; }

        public string Onu { get; set; }

        public string Serial { get; set; }

        public int Quantity { get; set; }

        public long StockId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
