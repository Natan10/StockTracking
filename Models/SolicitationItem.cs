namespace StockTracking.Models
{
    public class SolicitationItem : BaseEntity
    { 
        public int ProcessedQuantity { get; set; }

        public int RequiredQuantity { get; set; }

        public int SolicitationId { get; set; }
        
        public Solicitation Solicitation { get; set; }

        public StockItemEquipment? Equipment { get; set; }

        public StockItemMaterial? Material { get; set; }
    }
}
