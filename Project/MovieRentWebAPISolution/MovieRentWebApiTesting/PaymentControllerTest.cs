using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Controllers;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using NUnit.Framework;

namespace MovieRentWebApiTesting
{
    public class PaymentControllerTest
    {
        private Mock<IPaymentService> _mockPaymentService;
        private Mock<ILogger<PaymentController>> _mockLogger;
        private PaymentController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _mockLogger = new Mock<ILogger<PaymentController>>();
            _controller = new PaymentController(_mockPaymentService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetPayments_ReturnsOkResult_WhenPaymentsAreFound()
        {
            // Arrange
            var payments = new List<Payment>
    {
        new Payment { RentalId = 1,
                CustomerId = 1,
                PaymentType = "UPI",
                Amount = 100,
                PaymentDate = DateTime.Now },
    };

            _mockPaymentService.Setup(service => service.GetAllPayments())
                .ReturnsAsync(payments); 

            // Act
            var result = await _controller.GetPayments();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(payments, okResult.Value);
        }

        [Test]
        public async Task GetPayments_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockPaymentService.Setup(service => service.GetAllPayments())
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _controller.GetPayments();

            // Assert
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            var errorResponse = objectResult.Value as ErrorResponseDTO;
            Assert.IsNotNull(errorResponse);
            Assert.AreEqual(500, errorResponse.ErrorCode);
            Assert.AreEqual("Something went wrong", errorResponse.ErrorMessage);
        }

        [Test]
        public async Task MakePayment_ReturnsOkResult_WhenPaymentIsSuccessful()
        {
            // Arrange
            var paymentDto = new MakePaymentDTO
            {
                RentalId = 1,
                CustomerId = 1,
                PaymentType = "Credit Card",

            };
            var payment = new Payment
            {
                RentalId = 1,
                CustomerId = 1,
                PaymentType = "UPI",
                Amount = 100,
                PaymentDate = DateTime.Now
    };
            _mockPaymentService.Setup(service => service.GeneratePayment(It.IsAny<MakePaymentDTO>()))
                    .ReturnsAsync("complete payment");

            // Act
            var result = await _controller.MakePayment(paymentDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Test]
        public async Task MakePayment_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var paymentDto = new MakePaymentDTO { RentalId = 1, CustomerId = 1, PaymentType = "Credit Card" };
            _mockPaymentService.Setup(service => service.GeneratePayment(It.IsAny<MakePaymentDTO>()))
                .ThrowsAsync(new Exception("Payment failed"));

            // Act
            var result = await _controller.MakePayment(paymentDto);

            // Assert
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            var errorResponse = objectResult.Value as ErrorResponseDTO;
            Assert.IsNotNull(errorResponse);
            Assert.AreEqual(500, errorResponse.ErrorCode);
            Assert.AreEqual("Payment failed", errorResponse.ErrorMessage);
        }
    }
}
