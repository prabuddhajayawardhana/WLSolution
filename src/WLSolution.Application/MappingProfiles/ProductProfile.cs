using AutoMapper;
using WLSolution.Application.DTOs;
using WLSolution.Domain.Entities;

namespace WLSolution.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Create mappings between domain models and DTOs
        CreateMap<Product, ProductDto>()
            .ReverseMap();
        CreateMap<CategoryAveragePrice, CategoryAveragePriceDto>()
           .ReverseMap();
        CreateMap<HighestStockCategory, HighestStockCategoryDto>()
           .ReverseMap();
    }
}