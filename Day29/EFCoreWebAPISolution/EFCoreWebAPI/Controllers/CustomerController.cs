using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBasicService _customerBasicService;

        public CustomerController(ICustomerBasicService cutomerBasicService)
        {
            _customerBasicService = cutomerBasicService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO customer)
        {
            try
            {
                var customerId = await _customerBasicService.CreateCustomer(customer);
                return Ok(customerId);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
