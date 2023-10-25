using Microsoft.EntityFrameworkCore;

namespace StockTracking.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
    }
}
