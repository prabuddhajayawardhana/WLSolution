using WLSolution.Application.DTOs;

namespace WLSolution.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(Guid id);
    Task<int> CreateProductAsync(ProductDto product);
    Task<int> UpdateProductAsync(ProductDto product);
    Task<int> DeleteProductAsync(Guid id);
    Task<List<CategoryAveragePriceDto>> GetAveragePriceForAllCategoriesAsync();
    Task<List<HighestStockCategoryDto>> GetCategoriesWithHighestStockAsync();
}