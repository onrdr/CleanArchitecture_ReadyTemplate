using AutoMapper;
using Core.DTOs.Category;
using Core.DTOs.Product;
using Core.Entities;

namespace Infrastructure.Mappings.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductDto, Product>(); 

        CreateMap<CreateCategoryDto, Category>(); 
    }
}
