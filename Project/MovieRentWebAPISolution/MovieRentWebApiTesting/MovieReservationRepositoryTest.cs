using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentWebApiTesting
{
    public class MovieReservationRepositoryTest
    {
        MovieRentContext context;
        DbContextOptions options;
        ReservationRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("Test Database")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            repository = new ReservationRepository(context);
        }

        [Test]
        public async Task AddMovieReservation_Success()
        {
            var reservation = new Reservation
            {
                CustomerId = 2,
                MovieId = 1,
            };
            var result = await repository.Add(reservation);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ReservationId, 1);
        }

        [Test]
        public async Task AddMovieReservation_Fail_WhenTryingToReservedSameMovieAtTime()
        {
            var reservation = new Reservation
            {
                CustomerId = 1,
                MovieId = 1,
            };
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
            
            var exception = Assert.ThrowsAsync<InvalidOperationException>
                (async () => await repository.Add(reservation));

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains("You cannot reserve the same movie more than once at the same time."));
        }

        [Test]
        public async Task Delete_NotImplementation()
        {
            var exception = Assert.ThrowsAsync<NotImplementedException>
                (async () => await repository.Delete(1));

            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task GetReservations_ByReservationId()
        {
            var reservation = new Reservation
            {
                CustomerId = 3,
                MovieId = 4,
            };
            await repository.Add(reservation);
            var result = await repository.Get(1);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ReservationId == 1);
        }

        [Test]
        public async Task GetAllReservations_Success()
        {
            var reservation = new Reservation
            {
                CustomerId = 6,
                MovieId = 5,
            };
            await repository.Add(reservation);
            var result = await repository.GetAll();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public async Task GetAllReservations_EmptyCollectionException()
        {
            var _options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            var _context = new MovieRentContext((DbContextOptions<MovieRentContext>)_options);
            var _repository = new ReservationRepository(_context);

            var result = Assert.ThrowsAsync<EmptyCollectionException>( async () => await _repository.GetAll());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message == "Reservations Collection Empty");
        }

        [Test]
        public async Task Update_Success()
        {
            var _options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("Test1")
                .Options;
            var _context = new MovieRentContext((DbContextOptions<MovieRentContext>)_options);
            var _repository = new ReservationRepository(_context);
            var reservations = new List<Reservation>()
            {
                new Reservation
                {
                    ReservationId = 1,
                    ReservationDate = DateTime.Now,
                    CustomerId = 6,
                    MovieId = 5,
                    Status = ReservationStatus.Completed,
                },
                 new Reservation
                {
                    ReservationId = 2,
                    ReservationDate = DateTime.Now,
                    CustomerId = 3,
                    MovieId = 3,
                    Status = ReservationStatus.Pending,
                }
            };

            var updateReservation = new Reservation
            {
                Status = ReservationStatus.Completed,
            };

            await _context.Reservations.AddRangeAsync(reservations);
            await _context.SaveChangesAsync();

            var result = await _repository.Update(updateReservation, 2);
            Assert.IsNotNull(result);
            Assert.AreEqual(ReservationStatus.Completed, result.Status);
        }

        [Test]
        public async Task Update_Fail_WhenEnterWrongReservationID()
        {
            var _options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("Test")
                .Options;
            var _context = new MovieRentContext(_options);
            var _repository = new ReservationRepository(_context);
            var reservations = new List<Reservation>()
            {
                new Reservation
                {
                    ReservationId = 1,
                    ReservationDate = DateTime.Now,
                    CustomerId = 6,
                    MovieId = 5,
                    Status = ReservationStatus.Completed,
                },
                 new Reservation
                {
                    ReservationId = 2,
                    ReservationDate = DateTime.Now,
                    CustomerId = 3,
                    MovieId = 3,
                    Status = ReservationStatus.Pending,
                }
            };
            int key = 6;

            var updateReservation = new Reservation
            {
                Status = ReservationStatus.Completed,
            };

            await _context.Reservations.AddRangeAsync(reservations);
            await _context.SaveChangesAsync();

            var result = Assert.ThrowsAsync<Exception>(async () => await _repository.Update(updateReservation, key));
            Assert.IsNotNull(result);
            Assert.AreEqual($"There is no Movie Reservation with this ID: {key}", result.Message);
        }

        [Test]
        public async Task SaveChangesAsync()
        {
            await repository.SaveChangesAsync();
            Assert.Pass();
        }
    }

}

