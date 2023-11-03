using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockTracking.Models;

namespace StockTracking.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockItem> StockItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Stock>()
                .HasAlternateKey(e => e.Name);

            builder.Entity<StockItem>()
                .HasAlternateKey(e => e.Code);

            base.OnModelCreating(builder);
        }
        
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();  
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var currentTime = DateTime.UtcNow;
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity entity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach(var entry in entries ) {
                if(entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedAt = currentTime;
                }

                 ((BaseEntity)entry.Entity).UpdatedAt = currentTime;
            }
        }
       
    }
}
