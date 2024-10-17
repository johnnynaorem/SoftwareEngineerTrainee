using AutoMapper;
using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTO;
using EFCoreWebAPI.Repositories;

namespace EFCoreWebAPI.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IRepository<int, ProductImage> _productImageRepo;
        private readonly IMapper _mapper;

        public ProductImageService(IRepository<int, ProductImage> productImageRepository, IMapper mapper)
        {
            _productImageRepo = productImageRepository;
            _mapper = mapper;
        }

        public async Task<ProductImage> CreateProductImage(ProductImageDTO productImage)
        {
            ProductImage newProductImage = _mapper.Map<ProductImage>(productImage);
            var addedProductImage = await _productImageRepo.Add(newProductImage);
            return addedProductImage;
        }
    }
}
