using System.ComponentModel.DataAnnotations.Schema;

namespace StockTracking.Models
{
    public class Employee : BaseEntity
    {
        public string Id { get; set; }

        public EEmployeeRole Role { get; set; } = EEmployeeRole.USER;
    }
}
