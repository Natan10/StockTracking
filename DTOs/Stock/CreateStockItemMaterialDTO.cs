using System.ComponentModel.DataAnnotations;

namespace StockTracking.DTOs.Stock
{
    public class CreateStockItemMaterialDTO
    {
        [Required(ErrorMessage = "Insira um Nome valido para o equipamento")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Insira um codigo valido")]
        public string Code { get; set; }

        [Required]
        public string MeasurementUnit { get; set; }

        [Required]
        public int Quantity {  get; set; }

        [Required]
        public long StockId { get; set; }
    }
}
