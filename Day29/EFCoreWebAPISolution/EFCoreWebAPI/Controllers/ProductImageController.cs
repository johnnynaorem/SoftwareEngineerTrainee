using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _service;

        public ProductImageController(IProductImageService productImageService)
        {
            _service = productImageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(ProductImageDTO productImage)
        {
            try
            {
               var newProductImage = await _service.CreateProductImage(productImage);
                return Ok(newProductImage);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
