using Microsoft.EntityFrameworkCore;
namespace ProductApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map C# models directly to your exact SQL table names
            modelBuilder.Entity<Product>().ToTable("Product_s");
        }
    }
}