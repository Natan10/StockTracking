namespace StockTracking.Models
{
    public class SolicitationItem : BaseEntity
    { 
        public int ProcessedQuantity { get; set; }

        public int RequiredQuantity { get; set; }

        public long SolicitationId { get; set; }
        
        public Solicitation Solicitation { get; set; }

        public long? EquipmentId { get; set; }

        public StockItemEquipment? Equipment { get; set; }

        public long? MaterialId {  get; set; }
        
        public StockItemMaterial? Material { get; set; }
    }
}
