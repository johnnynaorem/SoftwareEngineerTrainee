using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService) {
            _productService = productService;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            try
            {
                var productId = await _productService.CreateProduct(product);
                return Ok(productId);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("UpdateProductPrice")]

        public async Task<IActionResult> UpdateProductPrice(float price, int productId)
        {
            try
            {
                var product = await _productService.UpdateProductPrice(price, productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
