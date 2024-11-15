using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.EmailModels;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Repositories;
using MovieRentWebAPI.Services;
using NUnit.Framework;

namespace MovieRentWebApiTesting
{
    public class CustomerServiceTests
    {
        private Mock<IRepository<int, Customer>> _mockCustomerRepo;
        private Mock<IRepository<int, Movie>> _mockMovieRepo;
        private Mock<IRentalService> _mockRentalService;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<ILogger<CustomerService>> _mockLogger;
        private CustomerService _customerService;

        [SetUp]
        public void Setup()
        {
            // Initialize mocks for dependencies
            _mockCustomerRepo = new Mock<IRepository<int, Customer>>();
            _mockMovieRepo = new Mock<IRepository<int, Movie>>();
            _mockRentalService = new Mock<IRentalService>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockLogger = new Mock<ILogger<CustomerService>>();

            // Instantiate the service with the mocked dependencies
            _customerService = new CustomerService(
                _mockCustomerRepo.Object,
                _mockLogger.Object,
                _mockEmailSender.Object,
                _mockRentalService.Object,
                _mockMovieRepo.Object
            );
        }

        #region CreateCustomer Tests

        [Test]
        public async Task CreateCustomer_ShouldCreateCustomerAndSendEmail()
        {
            // Arrange
            var newCustomer = new CreateCustomerDTO
            {
                FullName = "John Doe",
                Address = "123 Main St",
                PhoneNumber = "123-456-7890",
                UserId = 1
            };

            var customer = new Customer
            {
                CustomerId = 1,
                FullName = "John Doe",
                Address = "123 Main St",
                PhoneNumber = "123-456-7890",
                UserId = 1
            };

            _mockCustomerRepo.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(customer);

            // Act
            var customerId = await _customerService.CreateCustomer(newCustomer);

            // Assert
            Assert.AreEqual(1, customerId);
            _mockEmailSender.Verify(sender => sender.SendEmail(It.IsAny<Message>()), Times.Once);
        }

        #endregion

        #region PickUpMovie Tests

        [Test]
        public async Task PickUpMovie_ShouldUpdateRentalStatusAndSendEmail()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var movie = new Movie { MovieId = 1, Title = "Inception", AvailableCopies=10 };
            var rental = new Rental
            {
                RentalId = 1,
                CustomerId = 1,
                MovieId = 1,
                Status = RentalStatus.Confirmed,
                RentalFee = 10.00
            };
            var update_rental = new Rental
            {
                RentalId = 1,
                CustomerId = 1,
                MovieId = 1,
                Status = RentalStatus.Active,
                RentalFee = 10.00,
            };

            var pickUpDto = new PickUpMovieDTO
            {
                RentId = 1,
                CustomerId = 1,
                MovieId = 1
            };

            _mockCustomerRepo.Setup(repo => repo.Get(1)).ReturnsAsync(customer);
            _mockMovieRepo.Setup(repo => repo.Get(1)).ReturnsAsync(movie);
            _mockRentalService.Setup(service => service.GetRentals()).ReturnsAsync(new List<Rental> { rental });
            _mockRentalService.Setup(service => service.Update(It.IsAny<RentalUpdateRequestDTO>())).ReturnsAsync(update_rental);


            // Act
            var result = await _customerService.PickUpMovie(pickUpDto);

            // Assert
            Assert.AreEqual(RentalStatus.Active, result.Status);
            _mockEmailSender.Verify(sender => sender.SendEmail(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public void PickUpMovie_ShouldThrowMoviePickUpException_WhenRentalStatusIsNotConfirmed()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var movie = new Movie { MovieId = 1, Title = "Inception" };
            var rental = new Rental
            {
                RentalId = 1,
                CustomerId = 1,
                MovieId = 1,
                Status = RentalStatus.Pending, 
                RentalFee = 10.00
            };

            var pickUpDto = new PickUpMovieDTO
            {
                RentId = 1,
                CustomerId = 1,
                MovieId = 1
            };

            _mockCustomerRepo.Setup(repo => repo.Get(1)).ReturnsAsync(customer);
            _mockMovieRepo.Setup(repo => repo.Get(1)).ReturnsAsync(movie);
            _mockRentalService.Setup(service => service.GetRentals()).ReturnsAsync(new List<Rental> { rental });

            // Act & Assert
            var exception = Assert.ThrowsAsync<MoviePickUpException>(
                async () => await _customerService.PickUpMovie(pickUpDto)
            );
            Assert.AreEqual("Cannot Pick Up Movie, Because The Movie is not in Reserved by You or Your Rental status is not Completed", exception.Message);
        }

        #endregion

        #region ReturnMovie Tests

        [Test]
        public async Task ReturnMovie_ShouldUpdateRentalStatusAndSendEmail()
        {
            // Arrange
            var rental = new Rental
            {
                RentalId = 1,
                CustomerId = 1,
                MovieId = 1,
                Status = RentalStatus.Active,
                RentalFee = 10.00,
                RentalDate = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(2)
            };

            var returnDto = new ReturnMovieResquestDTO
            {
                RentId = 1,
                CustomerId = 1
            };

            _mockRentalService.Setup(service => service.GetRentals()).ReturnsAsync(new List<Rental> { rental });
            _mockRentalService.Setup(service => service.Update(It.IsAny<RentalUpdateRequestDTO>())).ReturnsAsync(rental);
            _mockCustomerRepo.Setup(repo => repo.Get(1)).ReturnsAsync(new Customer { CustomerId = 1, FullName = "John Doe" });
            _mockMovieRepo.Setup(repo => repo.Get(1)).ReturnsAsync(new Movie { MovieId = 1, Title = "Inception" });

            // Act
            var result = await _customerService.ReturnMovie(returnDto);

            // Assert
            Assert.AreEqual("Successfully Return Movie, MovieID: 1", result.Message);
            _mockEmailSender.Verify(sender => sender.SendEmail(It.IsAny<Message>()), Times.Once);
        }

        //[Test]
        //public async Task ReturnMovie_ShouldThrowAnException_Test()
        //{
        //    var rental = new Rental
        //    {
        //        RentalId = 1,
        //        CustomerId = 1,
        //        MovieId = 1,
        //        Status = RentalStatus.Active,
        //        RentalFee = 10.00,
        //        RentalDate = DateTime.Now.AddDays(-5),
        //        DueDate = DateTime.Now.AddDays(2)
        //    };

        //    var returnDto = new ReturnMovieResquestDTO
        //    {
        //        RentId = 1,
        //        CustomerId = 1
        //    };

        //    _mockRentalService.Setup(s => s.GetRentals()).ThrowsAsync(new Exception("Error"));

        //    var result = await _customerService.ReturnMovie(returnDto);
        //    Exception.Equals("Error", result.Message);
        //}

        #endregion

        #region GetAllCustomer Tests

        [Test]
        public async Task GetAllCustomer_ShouldReturnListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FullName = "John Doe", Address = "123 Main St", PhoneNumber = "123-456-7890" },
                new Customer { CustomerId = 2, FullName = "Jane Doe", Address = "456 Oak St", PhoneNumber = "987-654-3210" }
            };

            _mockCustomerRepo.Setup(repo => repo.GetAll()).ReturnsAsync(customers);

            // Act
            var result = await _customerService.GetAllCustomer();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region GetCustomerById Tests

        [Test]
        public async Task GetCustomerById_ShouldReturnCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, FullName = "John Doe", Address = "123 Main St", PhoneNumber = "123-456-7890" };
            _mockCustomerRepo.Setup(repo => repo.Get(1)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetCustomerById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.CustomerId);
        }

        #endregion

        #region UpdateCustomer Tests

        [Test]
        public async Task UpdateCustomer_ShouldUpdateCustomerDetailsAndSendEmail()
        {
            // Arrange
            var updateDto = new CreateCustomerDTO
            {
                FullName = "Johnathan Doe",
                Address = "123 Main St",
                PhoneNumber = "123-456-7890",
                UserId = 1
            };

            var updatedCustomer = new Customer
            {
                CustomerId = 1,
                FullName = "Johnathan Doe",
                Address = "123 Main St",
                PhoneNumber = "123-456-7890",
                UserId = 1
            };

            _mockCustomerRepo.Setup(repo => repo.Update(It.IsAny<Customer>(), 1)).ReturnsAsync(updatedCustomer);

            // Act
            var result = await _customerService.UpdateCustomer(updateDto, 1);

            // Assert
            Assert.AreEqual(1, result);
            _mockEmailSender.Verify(sender => sender.SendEmail(It.IsAny<Message>()), Times.Once);
        }

        #endregion
    }
}
