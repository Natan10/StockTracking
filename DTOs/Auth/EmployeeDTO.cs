using StockTracking.Models;

namespace StockTracking.DTOs.Auth
{
    public class EmployeeDTO
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public EEmployeeRole Role { get; set; }

    }
}
