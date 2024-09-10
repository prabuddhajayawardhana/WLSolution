using Microsoft.EntityFrameworkCore;
using WLSolution.Domain.Entities;

namespace WLSolution.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{

    public DbSet<Product> Products { get; set; }
    public DbSet<CategoryAveragePrice> CategoryAveragePrices { get; set; }
    public DbSet<HighestStockCategory> HighestStockCategories { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryAveragePrice>()
            .HasNoKey()
            .ToView(null);

        modelBuilder.Entity<HighestStockCategory>()
            .HasNoKey()
            .ToView(null);

        SeedData.SeedProducts(modelBuilder);
    }

}
