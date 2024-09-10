using Microsoft.EntityFrameworkCore;
using WLSolution.Domain.Entities;
using WLSolution.Domain.Interfaces;
using WLSolution.Infrastructure.Persistence;

namespace WLSolution.Infrastructure.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<CategoryAveragePrice>> GetAveragePriceForAllCategoriesAsync()
    {
        return await _context.CategoryAveragePrices
            .FromSqlRaw("EXEC GetAveragePriceForAllCategories")
            .ToListAsync();
    }

    public async Task<List<HighestStockCategory>> GetCategoriesWithHighestStockAsync()
    {
        var categoriesWithHighestStock = await _context
            .HighestStockCategories
            .FromSqlRaw("EXEC GetCategoriesWithHighestStock")
            .ToListAsync();

        return categoriesWithHighestStock;
    }

}