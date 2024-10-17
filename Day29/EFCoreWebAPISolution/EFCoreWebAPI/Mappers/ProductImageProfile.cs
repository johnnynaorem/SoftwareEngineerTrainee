using AutoMapper;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTO;

namespace EFCoreWebAPI.Mappers
{
    public class ProductImageProfile:Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImage, ProductImageDTO>();
            CreateMap<ProductImageDTO, ProductImage>();

        }
    }
}
