using Microsoft.Extensions.Logging;
using Moq;
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
using MovieRentWebAPI.EmailModels;

namespace MovieRentWebApiTesting
{
    internal class MovieReservationServiceTest
    {
        private Mock<IRepository<int, Reservation>> _mockReservationRepo;
        private Mock<IRepository<int, Customer>> _mockCustomerRepo;
        private Mock<IRepository<int, Movie>> _mockMovieRepo;
        private Mock<IEmailSender> _mockEmailSender;
        private Mock<ILogger<MovieReservationService>> _mockLogger;
        private MovieReservationService _movieReservationService;

        [SetUp]
        public void Setup()
        {
            _mockReservationRepo = new Mock<IRepository<int, Reservation>>();
            _mockCustomerRepo = new Mock<IRepository<int, Customer>>();
            _mockMovieRepo = new Mock<IRepository<int, Movie>>();
            _mockEmailSender = new Mock<IEmailSender>();
            _mockLogger = new Mock<ILogger<MovieReservationService>>();

            _movieReservationService = new MovieReservationService(
                _mockReservationRepo.Object,
                _mockCustomerRepo.Object,
                _mockMovieRepo.Object,
                _mockLogger.Object,
                _mockEmailSender.Object
            );
        }

        [Test]
        public async Task ReserverMovie_ShouldThrowException_WhenMovieNotFound()
        {
            var reserveMovieDTO = new ReserveMovieDTO
            {
                CustomerId = 1,
                MovieId = 100,
                ReservationDate = DateTime.Now
            };
            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };

            _mockReservationRepo.Setup(repo => repo.Add(It.IsAny<Reservation>())).ReturnsAsync(new Reservation
            {
                ReservationId = 1,
                CustomerId = reserveMovieDTO.CustomerId,
                MovieId = reserveMovieDTO.MovieId,
                ReservationDate = reserveMovieDTO.ReservationDate
            });
            _mockCustomerRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(customer);
            _mockMovieRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((Movie)null);

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _movieReservationService.ReserverMovie(reserveMovieDTO)
            );
            Assert.AreEqual("Movie not found", exception.Message);
        }

        [Test]
        public async Task ReserverMovie_ShouldThrowException_WhenCustomerNotFound()
        {
            var reserveMovieDTO = new ReserveMovieDTO
            {
                CustomerId = 1,
                MovieId = 1,
                ReservationDate = DateTime.Now
            };

            _mockMovieRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(new Movie { MovieId = 1, Title = "Test Movie" });
            _mockCustomerRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((Customer)null); // Customer is not found

            _mockReservationRepo.Setup(repo => repo.Add(It.IsAny<Reservation>())).ReturnsAsync(new Reservation
            {
                ReservationId = 1,
                CustomerId = reserveMovieDTO.CustomerId,
                MovieId = reserveMovieDTO.MovieId,
                ReservationDate = reserveMovieDTO.ReservationDate
            });
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _movieReservationService.ReserverMovie(reserveMovieDTO)
            );
            Assert.AreEqual("Customer not found", exception.Message);
        }


        [Test]
        public async Task ReserverMovie_ShouldSendEmail_WhenReservationIsSuccessful()
        {
            var reserveMovieDTO = new ReserveMovieDTO
            {
                CustomerId = 1,
                MovieId = 1,
                ReservationDate = DateTime.Now
            };

            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var movie = new Movie { MovieId = 1, Title = "Test Movie", Genre = "Action" };
            var reservation = new Reservation { ReservationId = 1, CustomerId = 1, MovieId = 1, ReservationDate = DateTime.Now };

            _mockMovieRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(movie);
            _mockCustomerRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(customer);
            _mockReservationRepo.Setup(repo => repo.Add(It.IsAny<Reservation>())).ReturnsAsync(reservation);

            var result = await _movieReservationService.ReserverMovie(reserveMovieDTO);

            Assert.AreEqual(reservation.ReservationId, result);
        }

        [Test]
        public async Task UpdateMovieReservationStatus_ShouldThrowException_WhenReservationNotFound()
        {
            var updateRequest = new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId = 100,
                Status = ReservationStatus.Active
            };

            _mockReservationRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((Reservation)null);

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _movieReservationService.UpdateMovieReservationStatus(updateRequest)
            );
            Assert.AreEqual("Reservation not found", exception.Message);
        }

        [Test]
        public async Task UpdateMovieReservationStatusActive_ShouldSendEmail_WhenStatusIsUpdated()
        {
            var updateRequest = new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId = 1,
                Status = ReservationStatus.Active
            };

            var reservation = new Reservation
            {
                ReservationId = 1,
                Status = ReservationStatus.Pending,
                MovieId = 1,
                CustomerId = 1,
                ReservationDate = DateTime.Now
            };

            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var movie = new Movie { MovieId = 1, Title = "Test Movie", Genre = "Action" };

            _mockReservationRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockReservationRepo.Setup(repo => repo.Update(It.IsAny<Reservation>(), It.IsAny<int>()))
                .ReturnsAsync((Reservation updatedReservation, int reservationId) =>
                {
                    updatedReservation.Status = ReservationStatus.Active;
                    return updatedReservation;
                });
            _mockMovieRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(movie);
            _mockCustomerRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(customer);

            var result = await _movieReservationService.UpdateMovieReservationStatus(updateRequest);

            Assert.AreEqual(ReservationStatus.Active.ToString(), result.Status);
            _mockEmailSender.Verify(email => email.SendEmail(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public async Task UpdateMovieReservationStatusCompleted_ShouldSendEmail_WhenStatusIsUpdated()
        {
            var updateRequest = new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId = 4,
                Status = ReservationStatus.Completed
            };

            var reservation = new Reservation
            {
                ReservationId = 4,
                Status = ReservationStatus.Pending,
                MovieId = 3,
                CustomerId = 4,
                ReservationDate = DateTime.Now
            };

            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var movie = new Movie { MovieId = 1, Title = "Test Movie", Genre = "Action" };

            _mockReservationRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockReservationRepo.Setup(repo => repo.Update(It.IsAny<Reservation>(), It.IsAny<int>()))
                .ReturnsAsync((Reservation updatedReservation, int reservationId) =>
                {
                    updatedReservation.Status = ReservationStatus.Completed;
                    return updatedReservation;
                });
            _mockMovieRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(movie);
            _mockCustomerRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(customer);

            var result = await _movieReservationService.UpdateMovieReservationStatus(updateRequest);

            Assert.AreEqual(ReservationStatus.Completed.ToString(), result.Status);
            _mockEmailSender.Verify(email => email.SendEmail(It.IsAny<Message>()), Times.Once);
        }
        [Test]
        public async Task UpdateMovieReservationStatusExpired_ShouldSendEmail_WhenStatusIsUpdated()
        {
            var updateRequest = new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId = 4,
                Status = ReservationStatus.Completed
            };

            var reservation = new Reservation
            {
                ReservationId = 4,
                Status = ReservationStatus.Pending,
                MovieId = 3,
                CustomerId = 4,
                ReservationDate = DateTime.Now
            };

            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var movie = new Movie { MovieId = 1, Title = "Test Movie", Genre = "Action" };

            _mockReservationRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockReservationRepo.Setup(repo => repo.Update(It.IsAny<Reservation>(), It.IsAny<int>()))
                .ReturnsAsync((Reservation updatedReservation, int reservationId) =>
                {
                    updatedReservation.Status = ReservationStatus.Expired;
                    return updatedReservation;
                });
            _mockMovieRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(movie);
            _mockCustomerRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(customer);

            var result = await _movieReservationService.UpdateMovieReservationStatus(updateRequest);

            Assert.AreEqual(ReservationStatus.Expired.ToString(), result.Status);
            _mockEmailSender.Verify(email => email.SendEmail(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public async Task UpdateMovieReservationStatusNotAvailable_ShouldSendEmail_WhenStatusIsUpdated()
        {
            var updateRequest = new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId = 4,
                Status = ReservationStatus.Completed
            };

            var reservation = new Reservation
            {
                ReservationId = 4,
                Status = ReservationStatus.Pending,
                MovieId = 3,
                CustomerId = 4,
                ReservationDate = DateTime.Now
            };

            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var movie = new Movie { MovieId = 1, Title = "Test Movie", Genre = "Action" };

            _mockReservationRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockReservationRepo.Setup(repo => repo.Update(It.IsAny<Reservation>(), It.IsAny<int>()))
                .ReturnsAsync((Reservation updatedReservation, int reservationId) =>
                {
                    updatedReservation.Status = ReservationStatus.Not_Available;
                    return updatedReservation;
                });
            _mockMovieRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(movie);
            _mockCustomerRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(customer);

            var result = await _movieReservationService.UpdateMovieReservationStatus(updateRequest);

            Assert.AreEqual(ReservationStatus.Not_Available.ToString(), result.Status);
            _mockEmailSender.Verify(email => email.SendEmail(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public async Task UpdateMovieReservationStatusCancelled_ShouldSendEmail_WhenStatusIsUpdated()
        {
            var updateRequest = new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId = 6,
                Status = ReservationStatus.Cancelled
            };

            var reservation = new Reservation
            {
                ReservationId = 6,
                Status = ReservationStatus.Pending,
                MovieId = 6,
                CustomerId = 3,
                ReservationDate = DateTime.Now
            };

            var customer = new Customer { CustomerId = 1, FullName = "John Doe" };
            var movie = new Movie { MovieId = 1, Title = "Test Movie", Genre = "Action" };

            _mockReservationRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(reservation);
            _mockReservationRepo.Setup(repo => repo.Update(It.IsAny<Reservation>(), It.IsAny<int>()))
                .ReturnsAsync((Reservation updatedReservation, int reservationId) =>
                {
                    updatedReservation.Status = ReservationStatus.Cancelled;
                    return updatedReservation;
                });
            _mockMovieRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(movie);
            _mockCustomerRepo.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(customer);

            var result = await _movieReservationService.UpdateMovieReservationStatus(updateRequest);

            Assert.AreEqual(ReservationStatus.Cancelled.ToString(), result.Status);
            _mockEmailSender.Verify(email => email.SendEmail(It.IsAny<Message>()), Times.Once);
        }

        [Test]
        public async Task GetAll_Success()
        {
            var reservations = new List<Reservation>()
            {
                new Reservation
                {
                    ReservationId = 1,
                    Status = ReservationStatus.Pending,
                    MovieId = 1,
                    CustomerId = 1,
                    ReservationDate = DateTime.Now
                },
                new Reservation
                {
                    ReservationId = 2,
                    Status = ReservationStatus.Completed,
                    MovieId = 2,
                    CustomerId = 2,
                    ReservationDate = DateTime.Now
                }
            };

            _mockReservationRepo.Setup(repo => repo.GetAll())
                .ReturnsAsync(reservations);

            var result = await _movieReservationService.GetAll();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);
        }
    }
}
