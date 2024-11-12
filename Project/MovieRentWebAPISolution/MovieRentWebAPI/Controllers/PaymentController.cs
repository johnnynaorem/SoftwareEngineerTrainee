using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet("GetAllPayments")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPayments()
        {
            try
            {
                var response = await _paymentService.GetAllPayments();
                return Ok(response);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpGet("GetAllPaymentByCustomer")]
        [Authorize(Roles = "Admin,user")]
        public async Task<IActionResult> GetAllPaymentByCustomer(int id)
        {
            try
            {
                var payments = await _paymentService.GetAllPayments();
                var response = (from payment in payments where payment.CustomerId == id select new { payment.CustomerId, payment.RentalId, payment.Amount, payment.PaymentType, payment.PaymentDate }).ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpPost("MakePayment")]
        //[Authorize]
        public async Task<IActionResult> MakePayment(MakePaymentDTO payment)
        {
            try
            {
                var response = await _paymentService.GeneratePayment(payment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
