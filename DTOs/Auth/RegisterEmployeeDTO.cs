using StockTracking.Models;
using System.ComponentModel.DataAnnotations;

namespace StockTracking.DTOs.Auth
{
    public class RegisterEmployeeDTO
    {
        public string? Username { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} é invalido")]
        public  string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        public EEmployeeRole? Role { get; set; } 
    }
}
