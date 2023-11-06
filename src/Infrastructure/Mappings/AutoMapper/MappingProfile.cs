using ApplicationCore.DTOs.Category;
using ApplicationCore.DTOs.Product;
using AutoMapper; 
using ApplicationCore.Entities;

namespace Infrastructure.Mappings.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<ViewProductDto, Product>().ReverseMap();

        CreateMap<CreateCategoryDto, Category>();
        CreateMap<ViewCategoryDto, Category>().ReverseMap();
        CreateMap<ViewCategoryWithProductsDto, Category>().ReverseMap();
    }
}
