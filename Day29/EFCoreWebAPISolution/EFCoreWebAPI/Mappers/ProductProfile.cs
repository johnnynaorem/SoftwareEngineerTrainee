using AutoMapper;
using EFCoreWebAPI.Models.DTO;
using EFCoreWebAPI.Models;

namespace EFCoreWebAPI.Mappers
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
