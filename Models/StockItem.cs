namespace StockTracking.Models;

public abstract class StockItem : BaseEntity {
    public int StockId { get; set; }

    public virtual Stock Stock { get; set; }

    public int Quantity { get; set; }
}
