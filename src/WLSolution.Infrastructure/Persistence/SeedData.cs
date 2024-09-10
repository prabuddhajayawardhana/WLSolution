using Microsoft.EntityFrameworkCore;
using WLSolution.Domain.Entities;

namespace WLSolution.Infrastructure.Persistence;

public static class SeedData
{
    public static void SeedProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
           new Product { Name = "Product A", Category = "Electronics", Price = 299.99m, Stock = 50 },
           new Product { Name = "Product B", Category = "Electronics", Price = 199.99m, Stock = 30 },
           new Product { Name = "Product C", Category = "Clothing", Price = 49.99m, Stock = 100 },
           new Product { Name = "Product D", Category = "Clothing", Price = 79.99m, Stock = 80 },
           new Product { Name = "Product E", Category = "Furniture", Price = 399.99m, Stock = 20 }
       );
    }
}
