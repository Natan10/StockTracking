using System.ComponentModel.DataAnnotations.Schema;

namespace StockTracking.Models
{
    public class Employee
    {
        public string Id { get; set; }

        public EEmployeeRole Role { get; set; } = EEmployeeRole.USER;

        public List<Solicitation> ReviewerSolicitations { get; set; } 

        public List<Solicitation> RequesterSolicitations { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
