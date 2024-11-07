using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Controllers;
using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.EmailService;
using MovieRentWebAPI.Exceptions;
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
    public class CustomerControllerTest
    {
        private DbContextOptions<MovieRentContext> options;
        private MovieRentContext movieRentContext;
        private CustomerRepository customerRepository;
        private RentalRepository rentalRepository;
        private MovieRepository movieRepository;
        private ReservationRepository reservationRepository;
        //private Mock<ICustomerService> _mockCustomerService;
        private CustomerService customerService;
        private IRentalService rentalService;
        private Mock<ILogger<CustomerController>> _mockLogger;
        private Mock<ILogger<RentalService>> _mockLoggerRentalService;
        private Mock<ILogger<MovieReservationService>> _mockLoggerMovieReservationService;
        private Mock<ILogger<CustomerService>> _mockLoggerCustomerService;
        private IReservationService reservationService;
        private CustomerController _controller;
        private Mock<IMapper> mapper;
        private Mock<IEmailSender> _mockEmailSender;

        [SetUp]
        public void SetUp()
        {

            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            movieRentContext = new MovieRentContext(options);
            customerRepository = new CustomerRepository(movieRentContext);
            rentalRepository = new RentalRepository(movieRentContext);
            movieRepository = new MovieRepository(movieRentContext);
            reservationRepository = new ReservationRepository(movieRentContext);
            _mockEmailSender = new Mock<IEmailSender>();
            _mockLoggerMovieReservationService = new Mock<ILogger<MovieReservationService>>();
            _mockLoggerRentalService = new Mock<ILogger<RentalService>>();
            _mockLoggerCustomerService = new Mock<ILogger<CustomerService>>();
            reservationService = new MovieReservationService(reservationRepository,customerRepository,movieRepository, _mockLoggerMovieReservationService.Object, _mockEmailSender.Object);
            rentalService = new RentalService(rentalRepository, movieRepository, _mockLoggerRentalService.Object, reservationService, _mockEmailSender.Object, customerRepository);
            customerService = new CustomerService(customerRepository, _mockLoggerCustomerService.Object, _mockEmailSender.Object, rentalService, movieRepository);
            //_mockCustomerService = new Mock<ICustomerService>();
            _mockLogger = new Mock<ILogger<CustomerController>>();
            _controller = new CustomerController(customerService, _mockLogger.Object);
            mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task RegisterCustomer_ShouldReturnOk_WhenValidRequest()
        {
            // Arrange
            var createCustomerDto = new CreateCustomerDTO
            {
                FullName = "John Doe",
                Address = "123 Main St",
                PhoneNumber = "9876543210",
                UserId = 1
            };

            var customerEntity = new Customer
            {
                FullName = "John Doe",
                Address = "123 Main St",
                PhoneNumber = "9876543210",
                UserId = 1
            };

            mapper.Setup(m => m.Map<Customer>(createCustomerDto)).Returns(customerEntity);

            // Act
            var result = await _controller.RegisterCustomer(createCustomerDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task RegisterCustomer_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Arrange
            var createCustomerDto = new CreateCustomerDTO();
            _controller.ModelState.AddModelError("FullName", "Required"); 

            // Act
            var result = await _controller.RegisterCustomer(createCustomerDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            var response = badRequestResult.Value as ErrorResponseDTO;
            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.ErrorCode);
        }

        // Test UpdateCustomerProfile (PATCH)
        [Test]
        public async Task UpdateCustomerProfile_ShouldReturnOk_WhenValidRequest()
        {
            
            var updateCustomerDto = new CreateCustomerDTO
            {
                FullName = "John Doe Updated",
                Address = "456 Main St",
                PhoneNumber = "9876543210",
                UserId = 1
            };

            await RegisterCustomer_ShouldReturnOk_WhenValidRequest();

            // Act
            var result = await _controller.UpdateCustomerProfile(updateCustomerDto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task UpdateCustomerProfile_ShouldReturnBadRequest_WhenInvalidRequest()
        {
            // Arrange
            var updateCustomerDto = new CreateCustomerDTO(); 
            _controller.ModelState.AddModelError("FullName", "Required");

            // Act
            var result = await _controller.UpdateCustomerProfile(updateCustomerDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            var response = badRequestResult.Value as ErrorResponseDTO;
            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.ErrorCode);
        }

        // Test GetAllCustomers (GET)
        [Test]
        public async Task GetAllCustomers_ShouldReturnOk_WhenValidRequest()
        {
            // Arrange
            await RegisterCustomer_ShouldReturnOk_WhenValidRequest();

            // Act
            var result = await _controller.GetAllCustomers();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task GetAllCustomers_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var _mockCustomerService = new Mock<ICustomerService>();

            _mockCustomerService.Setup(s => s.GetAllCustomer())
                .ThrowsAsync(new Exception("Internal Server Error"));

            // Act
            var result = await _controller.GetAllCustomers();

            // Assert
            var internalServerErrorResult = result as ObjectResult;
            Assert.IsNotNull(internalServerErrorResult);
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
        }

        [Test]
        public async Task UpdateCustomer_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var _mockCustomerService = new Mock<ICustomerService>();
            var controller = new CustomerController(_mockCustomerService.Object, null);

            var updateCustomerDto = new CreateCustomerDTO
            {
                FullName = "John Doe Updated",
                Address = "456 Main St",
                PhoneNumber = "9876543210",
                UserId = 1
            };

            _mockCustomerService.Setup(s => s.UpdateCustomer(updateCustomerDto, 1))
                .ThrowsAsync(new Exception("Internal Server Error"));

            // Act
            var result = await controller.UpdateCustomerProfile(updateCustomerDto);

            // Assert
            var internalServerErrorResult = result as ObjectResult;
            Assert.IsNotNull(internalServerErrorResult);
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
        }

        // Test PickUpMovie (PATCH)
        [Test]
        public async Task PickUpMovie_ShouldReturnOk_WhenValidRequest()
        {
            // Arrange
            var pickUp = new PickUpMovieDTO
            {
                CustomerId = 1,
                MovieId = 1,
                RentId = 1,
            };

            var rental = new Rental
            {
                RentalDate = DateTime.Now,
                CustomerId = 1,
                MovieId = 1,
                Status = RentalStatus.Confirmed,
                RentalFee = 100
            };

            var movie = new Movie
            {
                Title = "Title",
                Genre = "Genre",
                Rating = 2,
                ReleaseDate = DateTime.Now,
                Rental_Price = 100
            };

            await RegisterCustomer_ShouldReturnOk_WhenValidRequest();
            await movieRepository.Add(movie);
            await rentalRepository.Add(rental);

            // Act
            var result = await _controller.PickUpMovie(pickUp);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task PickUpMovie_ShouldReturnBadRequest_WhenMoviePickUpFails()
        {
            // Arrange
            var _mockCustomerService = new Mock<ICustomerService>();
            var controller = new CustomerController(_mockCustomerService.Object, null);

            var pickUp = new PickUpMovieDTO
            {
                CustomerId = 1,
                RentId = 1,
            };

            _mockCustomerService.Setup(s => s.PickUpMovie(pickUp))
                .ThrowsAsync(new MoviePickUpException("Movie pickup failed"));

            // Act
            var result = await controller.PickUpMovie(pickUp);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            var response = badRequestResult.Value as ErrorResponseDTO;
            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.ErrorCode);
            Assert.AreEqual("Movie pickup failed", response.ErrorMessage);
        }

        [Test]
        public async Task PickUpMovie_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var _mockCustomerService = new Mock<ICustomerService>();
            var controller = new CustomerController(_mockCustomerService.Object, null);
            var pickUpMovieDto = new PickUpMovieDTO
            {
                MovieId = 1,
                CustomerId = 1
            };
            _mockCustomerService.Setup(s => s.PickUpMovie(pickUpMovieDto))
                 .ThrowsAsync(new Exception("Internal Server Error"));

            // Act
            var result = await controller.PickUpMovie(pickUpMovieDto);

            // Assert
            var internalServerErrorResult = result as ObjectResult;
            Assert.IsNotNull(internalServerErrorResult);
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
        }
    }
}
