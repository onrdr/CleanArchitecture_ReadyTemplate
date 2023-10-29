using AutoMapper;
using Core.Entities;
using Core.Entities.DTOs.Category;
using Core.Entities.DTOs.Product;

namespace Infrastructure.Mappings.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductDto, Product>(); 

        CreateMap<CreateCategoryDto, Category>(); 
    }
}
