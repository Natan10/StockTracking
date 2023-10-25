using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockTracking.Models;

namespace StockTracking.Data
{
    public class DataContext : IdentityDbContext<IUser>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
    }
}
