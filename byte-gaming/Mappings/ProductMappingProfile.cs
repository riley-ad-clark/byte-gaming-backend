using AutoMapper;
using Entities.Entities;
namespace AdvancedCSLab02.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // maps for obscuring important info (id, and post date should not technically be public)
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
