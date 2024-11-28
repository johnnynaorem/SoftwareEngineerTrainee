using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Payments;
using Stripe;


namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger, IOptions<StripeSettings> stripeSettings)
        {
            _paymentService = paymentService;
            _logger = logger;
            _stripeSettings = stripeSettings.Value;
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
            catch (Exception ex)
            {
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
                var response = (from payment in payments where payment.Customer.CustomerId == id select new { payment.Customer.CustomerId, payment.RentalId, payment.Amount, payment.PaymentType, payment.PaymentDate }).ToList();
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

        public class Address
        {
            public string Line1 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
        }


        [HttpPost("create-payment-intent")]
        public IActionResult CreatePaymentIntent([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                // Set the API key from your secret key
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

                var options = new PaymentIntentCreateOptions
                {
                    Amount = paymentRequest.Amount,
                    Currency = paymentRequest.Currency,
                    Description = paymentRequest.Description, // Required for Indian exports

                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                    },

                    Shipping = new ChargeShippingOptions
                    {
                        Name = paymentRequest.CustomerName,
                        Address = new AddressOptions
                        {
                            Line1 = paymentRequest.CustomerAddress.Line1,
                            City = paymentRequest.CustomerAddress.City,
                            State = paymentRequest.CustomerAddress.State,
                            PostalCode = paymentRequest.CustomerAddress.PostalCode,
                            Country = paymentRequest.CustomerAddress.Country
                        }
                    }
                };

                // Create the PaymentIntent
                var service = new PaymentIntentService();
                PaymentIntent paymentIntent = service.Create(options);

                // Return the client secret to the frontend
                return Ok(new { clientSecret = paymentIntent.ClientSecret });
            }
            catch (StripeException e)
            {
                // Handle any errors gracefully
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
