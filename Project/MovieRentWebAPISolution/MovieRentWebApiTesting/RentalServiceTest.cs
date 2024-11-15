using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.Repositories;
using Castle.Core.Resource;
using Microsoft.VisualBasic;

namespace MovieRentWebApiTesting
{
    public class RentalServiceTest
    {
        private MovieRentContext _dbContext;
        private Mock<IReservationService> _mockReservationService;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<ILogger<RentalService>> _mockLogger;
        private RentalService _rentalService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new MovieRentContext(options);
            _mockReservationService = new Mock<IReservationService>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockLogger = new Mock<ILogger<RentalService>>();

            _rentalService = new RentalService(
                new RentalRepository(_dbContext),
                new MovieRepository(_dbContext),
                _mockLogger.Object,
                _mockReservationService.Object,
                _mockEmailSender.Object,
                new CustomerRepository(_dbContext)
            );
        }

        [Test]
        public async Task RentMovie_ShouldThrowInvalidOperationException_WhenMovieNotFound()
        {
            var rentMovieDTO = new RentMovieDTO
            {
                CustomerId = 1,
                MovieId = 100
            };

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _rentalService.RentMovie(rentMovieDTO));
            Assert.That(exception.Message, Is.EqualTo("Movie not Found"));
        }

        [Test]
        public async Task RentMovie_ShouldThrowInvalidOperationException_WhenCustomerNotFound()
        {
            var rentMovieDTO = new RentMovieDTO
            {
                CustomerId = 1,
                MovieId = 1
            };

            var movie = new Movie { MovieId = 1, Title = "Test Movie" };
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await _rentalService.RentMovie(rentMovieDTO));
            Assert.That(exception.Message, Is.EqualTo("Customer not Found"));
        }

        [Test]
        public async Task RentMovie_ShouldThrowMovieNotReservedException_WhenMovieNotReserved()
        {
            var rentMovieDTO = new RentMovieDTO
            {
                CustomerId = 1,
                MovieId = 1
            };

            var rentals = new List<Rental>
            {
                new Rental
                {
                    RentalId = 1,
                    CustomerId = 1,
                    MovieId = 1,
                    Status = RentalStatus.Active,
                    RentalDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                },
                new Rental
                {
                    RentalId = 2,
                    CustomerId = 2,
                    MovieId = 2,
                    Status = RentalStatus.Pending,
                    RentalDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                },
            };

            await _dbContext.Rentals.AddRangeAsync(rentals);
            await _dbContext.SaveChangesAsync();

            var movie = new Movie { MovieId = 1, Title = "Test Movie" };
            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var reservations = new List<Reservation>
            {
                new Reservation { MovieId = 1, Status = ReservationStatus.Active, CustomerId=1 }
            };

            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            _mockReservationService.Setup(service => service.GetAll()).ReturnsAsync(new List<Reservation>());
            _mockReservationService.Setup(service => service.UpdateMovieReservationStatus(It.IsAny<ReservedMovieStatusUpdateRequestDTO>()))
               .ReturnsAsync(new ReservedStatusUpdateResponseDTO
               {
                   Status = ReservationStatus.Completed.ToString(),
                   ReservationId = 1
               });
            var exception = Assert.ThrowsAsync<MovieNotReservedException>(async () => await _rentalService.RentMovie(rentMovieDTO));
            Assert.That(exception.Message, Is.EqualTo("You are not able to rent this movie because it is not reserved or the reservation status is not 'Active'"));
        }

        [Test]
        public async Task RentMovie_ShouldRentMovieSuccessfully_WhenValidData()
        {
            var rentMovieDTO = new RentMovieDTO
            {
                CustomerId = 1,
                MovieId = 1
            };

            var rentals = new List<Rental>
            {
                new Rental
                {
                    RentalId = 1,
                    CustomerId = 1,
                    MovieId = 1,
                    Status = RentalStatus.Active,
                    RentalDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                },
                new Rental
                {
                    RentalId = 2,
                    CustomerId = 2,
                    MovieId = 2,
                    Status = RentalStatus.Pending,
                    RentalDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                },
            };

            await _dbContext.Rentals.AddRangeAsync(rentals);
            await _dbContext.SaveChangesAsync();

            var movie = new Movie { MovieId = 1, Title = "Test Movie", Rental_Price = 10.0 };
            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var reservations = new List<Reservation>
            {
                new Reservation { MovieId = 1, Status = ReservationStatus.Active, CustomerId=1 }
            };

            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            _mockReservationService.Setup(service => service.GetAll()).ReturnsAsync(reservations);
            _mockReservationService.Setup(service => service.UpdateMovieReservationStatus(It.IsAny<ReservedMovieStatusUpdateRequestDTO>()))
                .ReturnsAsync(new ReservedStatusUpdateResponseDTO
                {
                    Status = ReservationStatus.Completed.ToString(),
                    ReservationId = 1
                });
            

            var result = await _rentalService.RentMovie(rentMovieDTO);
            Assert.IsNotNull(result);

            //Assert.That(result, Is.EqualTo("Successfully rented movie with ID: 1"));
        }

        [Test]
        public async Task UpdateRental_ShouldReturnUpdatedRental_WhenValidData()
        {
            var rentalUpdateDTO = new RentalUpdateRequestDTO
            {
                RentalId = 1,
                Status = RentalStatus.Returned,
                ReturnDate = DateTime.Now
            };

            var rental = new Rental
            {
                RentalId = rentalUpdateDTO.RentalId,
                CustomerId = 1,
                MovieId = 1,
                Status = RentalStatus.Pending,
                RentalDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
            };

            await _dbContext.Rentals.AddAsync(rental);
            await _dbContext.SaveChangesAsync();

            var result = await _rentalService.Update(rentalUpdateDTO);

            Assert.That(result.Status, Is.EqualTo(RentalStatus.Returned));
            Assert.That(result.RentalId, Is.EqualTo(rentalUpdateDTO.RentalId));
        }

        [Test]
        public async Task GetRentals_Success()
        {
            var rental = new Rental
            {
                RentalId = 1,
                CustomerId = 1,
                MovieId = 1,
                Status = RentalStatus.Pending,
                RentalDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
            };
            await _dbContext.Rentals.AddAsync(rental);
            await _dbContext.SaveChangesAsync();

            var result = await _rentalService.GetRentals();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
        }
    }
}
