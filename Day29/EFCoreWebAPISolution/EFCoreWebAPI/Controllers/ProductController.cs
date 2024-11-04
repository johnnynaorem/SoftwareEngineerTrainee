using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private readonly ILogger<ProductController> _logger;    

        public ProductController(IProductService productService, ILogger<ProductController> logger) {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("CreateProduct")]
        [Authorize]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            try
            {
                var productId = await _productService.CreateProduct(product);
                _logger.LogInformation("Product Added");
                return Ok(productId);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("UpdateProductPrice")]

        public async Task<IActionResult> UpdateProductPrice(float price, int productId)
        {
            try
            {
                var updateProduct = await _productService.UpdateProductPrice(price, productId);
                return Ok(updateProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
