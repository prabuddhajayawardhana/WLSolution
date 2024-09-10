using WLSolution.Domain.Entities;

namespace WLSolution.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<CategoryAveragePrice>> GetAveragePriceForAllCategoriesAsync();
    Task<List<HighestStockCategory>> GetCategoriesWithHighestStockAsync();
}
