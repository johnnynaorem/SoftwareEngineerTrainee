using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTO;

namespace EFCoreWebAPI.Interfaces
{
    public interface IProductImageService
    {
        Task<ProductImage> CreateProductImage(ProductImageDTO product);
    }
}
