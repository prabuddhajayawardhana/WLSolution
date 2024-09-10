using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WLSolution.Application.DTOs;
using WLSolution.Application.Interfaces;
using WLSolution.Domain.Entities;
using WLSolution.Domain.Interfaces;
using WLSolution.SharedKernel.Constants;
using WLSolution.SharedKernel.Validation;

namespace WLSolution.Application.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<IEnumerable<ProductDto>?> GetProductsAsync()
    {
        // Try to get the cached data
        if (_cache.TryGetValue(CacheKeys.ProductList, out IEnumerable<ProductDto>? cachedProducts))
        {
            return cachedProducts;
        }

        // Data not in cache, fetch it from the repository
        var products = await _productRepository.GetAllAsync();
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        // Set cache options
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // expiration of 5 minutes
        };

        // Save data to cache
        _cache.Set(CacheKeys.ProductList, productDtos, cacheEntryOptions);

        return productDtos;
    }

    public async Task<ProductDto> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        // Validate if the product was found
        Guard.ThrowIfObjectNotFound(product, nameof(product));

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<int> CreateProductAsync(ProductDto product)
    {
        var entity = _mapper.Map<Product>(product);

        // Ensure the entity was successfully mapped
        Guard.ThrowIfNull(entity, nameof(entity));

        await _productRepository.AddAsync(entity);
        
        // Clear the cache after creating a product
        _cache.Remove(CacheKeys.ProductList);

        // Save changes and validate the result
        var result = await _unitOfWork.SaveChangesAsync();
        Guard.ThrowIfInvalidOperation(result > 0, "Failed to create the product.");

        return result;
    }

    public async Task<int> UpdateProductAsync(ProductDto product)
    {
        var entity = _mapper.Map<Product>(product);
        _productRepository.Update(entity);

        // Clear the cache after updating a product
        _cache.Remove(CacheKeys.ProductList);

        // Save changes and validate the result
        var result = await _unitOfWork.SaveChangesAsync();
        Guard.ThrowIfInvalidOperation(result > 0, "Failed to update the product.");

        return result;
    }

    public async Task<int> DeleteProductAsync(Guid id)
    {
        await _productRepository.DeleteAsync(id);
        // Clear the cache after deleting a product
        _cache.Remove(CacheKeys.ProductList);

        // Save changes and validate the result
        var result = await _unitOfWork.SaveChangesAsync();
        Guard.ThrowIfInvalidOperation(result > 0, "Failed to delete the product.");

        return result;
    }

    public async Task<List<CategoryAveragePriceDto>?> GetAveragePriceForAllCategoriesAsync()
    {
        // Check if the data is already cached
        if (_cache.TryGetValue(CacheKeys.AvgPriceForAllCategories, out List<CategoryAveragePriceDto>? cachedAvgPriceForAllCategories))
        {
            return cachedAvgPriceForAllCategories;
        }

        // Data not in cache, fetch it from the repository
        var avgPrices = await _productRepository.GetAveragePriceForAllCategoriesAsync();

        // Validate if the repository returned valid data
        Guard.ThrowIfNull(avgPrices, nameof(avgPrices));

        var categoryAveragePrices = _mapper.Map<List<CategoryAveragePriceDto>>(avgPrices);

        // Ensure the mapping was successful
        Guard.ThrowIfNull(categoryAveragePrices, nameof(categoryAveragePrices));

        // Set cache options
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // expiration of 5 minutes
        };

        // Save data to cache
        _cache.Set(CacheKeys.AvgPriceForAllCategories, categoryAveragePrices, cacheEntryOptions);

        return categoryAveragePrices;
    }

    public async Task<List<HighestStockCategoryDto>?> GetCategoriesWithHighestStockAsync()
    {
        // Check if the data is already cached
        if (_cache.TryGetValue(CacheKeys.HighestStockCategories, out List<HighestStockCategoryDto>? cachedhighestStockCategoriesData))
        {
            return cachedhighestStockCategoriesData;
        }

        // Data not in cache, fetch it from the repository
        var highestStockCategories = await _productRepository.GetCategoriesWithHighestStockAsync();

        // Validate if the repository returned valid data
        Guard.ThrowIfNull(highestStockCategories, nameof(highestStockCategories));

        var highestStockCategoriesData = _mapper.Map<List<HighestStockCategoryDto>>(highestStockCategories);

        // Ensure the mapping was successful
        Guard.ThrowIfNull(highestStockCategoriesData, nameof(highestStockCategoriesData));

        // Set cache options
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // expiration of 5 minutes
        };

        // Save data to cache
        _cache.Set(CacheKeys.HighestStockCategories, highestStockCategoriesData, cacheEntryOptions);

        return highestStockCategoriesData;
    }

}
