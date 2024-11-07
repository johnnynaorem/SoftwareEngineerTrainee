using Castle.Core.Logging;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Repositories;
using MovieRentWebAPI.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentWebApiTesting
{
    internal class PaymentServiceTest
    {
        DbContextOptions options;
        MovieRentContext context;
        private Mock<IRepository<int, Payment>> _mockPaymentRepo;
        private Mock<IRepository<int, Rental>> _mockRentalRepo;
        private Mock<IRepository<int, Customer>> _mockCustomerRepo;
        private Mock<IRepository<int, Movie>> _mockMovieRepo;
        private Mock<IRentalService> _mockRentalService;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<ILogger<PaymentService>> _mockLogger;

        private PaymentService _paymentService;


        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            _mockPaymentRepo = new Mock<IRepository<int, Payment>>();
            _mockRentalRepo = new Mock<IRepository<int, Rental>>();
            _mockCustomerRepo = new Mock<IRepository<int, Customer>>();
            _mockMovieRepo = new Mock<IRepository<int, Movie>>();
            _mockRentalService = new Mock<IRentalService>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockLogger = new Mock<ILogger<PaymentService>>();

            _paymentService = new PaymentService(
                _mockPaymentRepo.Object,
                _mockRentalRepo.Object,
                _mockLogger.Object,
                _mockRentalService.Object,
                _mockEmailSender.Object,
                _mockCustomerRepo.Object,
                _mockMovieRepo.Object
            );

        }
        [Test]
        public async Task GeneratePayment_Success_Test()
        {
            // Arrange
            var paymentDto = new MakePaymentDTO
            {
                RentalId = 1,
                CustomerId = 1,
                PaymentType = "Credit Card",
                
            };

            var rental = new Rental
            {
                RentalId = 1,
                MovieId = 1,
                RentalFee = 100.00,
                RentalDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                Status = RentalStatus.Pending
            };

            var customer = new Customer { CustomerId = 1, FullName = "John Doe", Address = "testing" };
            var movie = new Movie { MovieId = 1, Title = "Movie Title", Rental_Price = 100};

            _mockRentalRepo.Setup(r => r.Get(paymentDto.RentalId)).ReturnsAsync(rental);
            _mockCustomerRepo.Setup(c => c.Get(paymentDto.CustomerId)).ReturnsAsync(customer);
            _mockMovieRepo.Setup(m => m.Get(rental.MovieId)).ReturnsAsync(movie);
            _mockPaymentRepo.Setup(p => p.Add(It.IsAny<Payment>())).ReturnsAsync(new Payment
            {
                RentalId = rental.RentalId,
                CustomerId = paymentDto.CustomerId,
                PaymentType = paymentDto.PaymentType,
                Amount = movie.Rental_Price,
                PaymentDate = DateTime.Now
            });

            _mockRentalService.Setup(r => r.Update(It.IsAny<RentalUpdateRequestDTO>())).ReturnsAsync(new Rental
            {
                RentalId = rental.RentalId,
                CustomerId = paymentDto.CustomerId,
                MovieId = movie.MovieId,
                RentalFee = movie.Rental_Price
            });

            // Act
            var result = await _paymentService.GeneratePayment(paymentDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Contains.Substring("Payment is Successfull"));
            
        }

        [Test]
        public async Task GeneratePayment_Failure_RentalNotFound_Test()
        {
            // Arrange
            var paymentDto = new MakePaymentDTO
            {
                RentalId = 999, // This rental ID does not exist
                CustomerId = 1,
                PaymentType = "Credit Card"
            };

            _mockRentalRepo.Setup(r => r.Get(paymentDto.RentalId)).ReturnsAsync((Rental)null);

            // Act & Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _paymentService.GeneratePayment(paymentDto)
            );

            Assert.That(exception.Message, Is.EqualTo("Not Rental Found with ID 999"));
        }

        [Test]
        public async Task GetAllPayment_Success()
        {
            await GeneratePayment_Success_Test();

            var result = _paymentService.GetAllPayments();
            Assert.IsNotNull(result);
        }
    }
}