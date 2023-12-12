namespace StockTracking.Models;

public abstract class StockItem : BaseEntity {
    public long StockId { get; set; }

    public virtual Stock Stock { get; set; }

    public int Quantity { get; set; }

        
    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }

    public void DecreaseQuantity(int quantity) {  
       if(quantity > Quantity) throw new Exception("O item não possui essa quantidade em estoque");
       Quantity -= quantity; 
    }
}
