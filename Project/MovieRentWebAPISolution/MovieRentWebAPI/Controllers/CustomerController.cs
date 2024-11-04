using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost("RegisterCustomer")]
        [Authorize]
        public async Task<IActionResult> RegisterCustomer(CreateCustomerDTO customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var addCustomer = await _customerService.CreateCustomer(customer);
                    return Ok(addCustomer);
                }
                else
                {
                    return BadRequest(new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = "one or more fields validate error"
                    });
                }
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorMessage = ex.ToString(),
                    ErrorCode = 500
                });
            }
        }

        [HttpPut("UpdateCustomerProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomerProfile(CreateCustomerDTO customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateCustomer = await _customerService.UpdateCustomer(customer, customer.UserId);
                    return Ok(updateCustomer);
                }
                else
                {
                    return BadRequest(new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = "one or more fields validate error"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorMessage = ex.ToString(),
                    ErrorCode = 500
                });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomer();
                return Ok(customers);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.ToString(),
                });
            }

        }
    }
}
