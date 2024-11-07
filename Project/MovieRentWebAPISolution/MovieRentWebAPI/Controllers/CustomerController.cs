using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Services;

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

        [HttpPatch("UpdateCustomerProfile")]
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
                    ErrorMessage = ex.Message.ToString(),
                    ErrorCode = 500
                });
            }
        }

        [HttpGet("GetAllCustomer")]
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

        [HttpPatch("PickUpMovie")]
        [Authorize]
        public async Task<IActionResult> PickUpMovie(PickUpMovieDTO pickUp)
        {
            try
            {
                var user = await _customerService.PickUpMovie(pickUp);
                return Ok(user);
            }
            catch (MoviePickUpException ex)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPatch("returnMovie")]
        [Authorize]
        public async Task<IActionResult> ReturnMovie(ReturnMovieResquestDTO returnMovie)
        {
            try
            {
                var user = await _customerService.ReturnMovie(returnMovie);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
