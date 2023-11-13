namespace StockTracking.Models
{
    public class SolicitationItem
    {
        public int Id { get; set; }

        public int ProcessedQuantity { get; set; }

        public int RequiredQuantity { get; set; }

        public int SolicitationId { get; set; }
        
        public Solicitation Solicitation { get; set; }

        public int StockItemId { get; set; }
        
        public StockItem  StockItem { get; set; }

    }
}
