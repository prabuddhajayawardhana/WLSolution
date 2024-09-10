using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using WLSolution.Application.DTOs;
using WLSolution.Domain.Entities;
using WLSolution.Domain.Interfaces;
using WLSolution.SharedKernel.Constants;
using WLSolution.Application.Services;

namespace WLSolution.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private Mock<IMemoryCache> _mockCache;
        private ProductService _productService;

        [TestInitialize]
        public void SetUp()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockCache = new Mock<IMemoryCache>();

            _productService = new ProductService(_mockProductRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object, _mockCache.Object);
        }

        [TestMethod]
        public async Task GetProductsAsync_ReturnsProductsFromRepository_WhenCacheIsNotAvailable()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Product A",
                    Category = "Electronics",
                    Price = 100.0m,
                    Stock = 10
                }
            };
            var productDtos = new List<ProductDto>
            {
                new ProductDto
                {
                    ProductId = products[0].ProductId,
                    Name = products[0].Name,
                    Category = products[0].Category,
                    Price = products[0].Price,
                    Stock = products[0].Stock
                }
            };
            _mockCache.Setup(x => x.CreateEntry(CacheKeys.ProductList)).Returns(Mock.Of<ICacheEntry>);
            _mockProductRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

            // Act
            var result = await _productService.GetProductsAsync();

            // Assert
            Assert.AreEqual(productDtos, result);
        }

        [TestMethod]
        public async Task GetProductByIdAsync_ReturnsMappedProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product
            {
                ProductId = productId,
                Name = "Product A",
                Category = "Electronics",
                Price = 100.0m,
                Stock = 10
            };
            var productDto = new ProductDto
            {
                ProductId = productId,
                Name = "Product A",
                Category = "Electronics",
                Price = 100.0m,
                Stock = 10
            };
            _mockProductRepository.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockMapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.AreEqual(productDto, result);
        }

        [TestMethod]
        public async Task DeleteProductAsync_DeletesProductAndClearsCache()
        {
            // Arrange
            var productId = Guid.NewGuid();

            // Set up the repository mock to simulate successful deletion
            _mockProductRepository.Setup(repo => repo.DeleteAsync(productId)).Returns(Task.CompletedTask);

            // Set up the UnitOfWork mock to simulate successful SaveChangesAsync
            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _productService.DeleteProductAsync(productId);

            // Assert
            Assert.AreEqual(1, result); // Ensure that the number of rows affected is as expected

            // Verify that the cache was cleared
            _mockCache.Verify(cache => cache.Remove(CacheKeys.ProductList), Times.Once);

            // Verify that SaveChangesAsync was called
            _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }


    }
}
