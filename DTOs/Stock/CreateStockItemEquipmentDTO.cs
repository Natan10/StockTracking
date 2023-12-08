using System.ComponentModel.DataAnnotations;

namespace StockTracking.DTOs.Stock
{
    public class CreateStockItemEquipmentDTO
    {
        [Required(ErrorMessage = "Insira uma Onu valida")]
        public string Onu { get; set; }

        [Required(ErrorMessage = "Insira um Numero de Serial valido")]
        public string Serial { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int StockId { get; set; }
    }
}
