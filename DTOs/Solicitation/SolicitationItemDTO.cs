namespace StockTracking.DTOs.Solicitation
{
    public class SolicitationItemDTO
    {
        public int ProcessedQuantity { get; set; }

        public int RequiredQuantity { get; set; }

        public long? EquipmentId { get; set; }

        public long? MaterialId { get; set; }
    }
}
