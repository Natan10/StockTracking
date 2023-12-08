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
    
        public DbSet<StockItemMaterial> StockItemMaterials { get; set; }
        
        public DbSet<StockItemEquipment> StockItemEquipments { get; set; }

        public DbSet<Solicitation> Solicitations { get; set; }
       
        public DbSet<SolicitationItem> SolicitationItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .HasMany(e => e.ReviewerSolicitations)
                .WithOne(e => e.Reviewer)
                .HasForeignKey(e => e.ReviewerId);

            builder.Entity<Employee>()
                .HasMany(e => e.RequesterSolicitations)
                .WithOne(e => e.Requester)
                .HasForeignKey(e => e.RequesterId);

            builder.Entity<Stock>()
                .HasAlternateKey(e => e.Name);

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
