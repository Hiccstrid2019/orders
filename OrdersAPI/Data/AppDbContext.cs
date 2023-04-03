using Microsoft.EntityFrameworkCore;

namespace OrdersAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> configurations) : base(configurations) {}
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasIndex(o => new {o.Number, o.ProviderId}).IsUnique();
        }
    }
}