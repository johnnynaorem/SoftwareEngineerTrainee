using Microsoft.EntityFrameworkCore;
using Moq;
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
    public class RentalRepositoryTest
    {
        DbContextOptions<MovieRentContext> options;
        MovieRentContext context;
        RentalRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            context = new MovieRentContext(options);
            repository = new RentalRepository(context);
        }

        [Test]
        public async Task AddRental_Success()
        {
            var rental = new Rental
            {
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                Status = RentalStatus.Pending,
                RentalDate = DateTime.Now,
            };

            var result = await repository.Add(rental);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.RentalId, 1);
        }

        [Test]
        public async Task AddRental_Exception()
        {
            var rental = new Rental
            {
                CustomerId = 1,
                RentalFee = 100,
                Status = RentalStatus.Pending,
                RentalDate = DateTime.Now,
            };

             context.Dispose();

            var exception = Assert.ThrowsAsync<CouldNotAddException>
                (async () => await repository.Add(rental));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Rental Add Failed");
        }

        [Test]
        public async Task GetRental_Success()
        {
            var rental = new Rental
            {
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                Status = RentalStatus.Pending,
                RentalDate = DateTime.Now,
            };

            await repository.Add(rental);
            var result = await repository.Get(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.RentalId);
        }

        [Test]
        public async Task GetAllRental_Success()
        {
            var rentals = new List<Rental>
            {
                new Rental {
                    RentalId = 1,
                    MovieId = 1,
                    CustomerId = 1,
                    RentalFee = 100,
                    Status = RentalStatus.Pending,
                    RentalDate = DateTime.Now,
                },

                new Rental{
                    RentalId = 2,
                    MovieId = 2,
                    CustomerId = 2,
                    RentalFee = 100,
                    Status = RentalStatus.Pending,
                    RentalDate = DateTime.Now,
                }
            };

            var result = await repository.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllRental_EmptyCollectionException()
        {
            var _options = new DbContextOptionsBuilder<MovieRentContext>()
               .UseInMemoryDatabase("Test1")
               .Options;

            var _context = new MovieRentContext(_options);
            var _repository = new RentalRepository(_context);
            var exception = Assert.ThrowsAsync<EmptyCollectionException>
                (async () => await _repository.GetAll());
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Rentals Collection Empty");
        }

        [Test]
        public async Task Delete_NotImplement()
        {
            var exception = Assert.ThrowsAsync<NotImplementedException>
                (async () => await repository.Delete(1));
            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task UpdateRental_Success()
        {
            var rental = new Rental
            {
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                Status = RentalStatus.Pending,
                RentalDate = DateTime.Now,
            };

            var updateRental = new Rental
            {
                ReturnDate = DateTime.Now,
                Status = RentalStatus.Returned,
            };
            await repository.Add(rental);
            var result = await repository.Update(updateRental,1);
            Assert.IsNotNull(result);
            Assert.NotNull(result.ReturnDate);
        }

        [Test]
        public async Task UpdateRental_KeyNotFound()
        {

            var updateRental = new Rental
            {
                ReturnDate = DateTime.Now,
                Status = RentalStatus.Returned,
            };
            var result = Assert.ThrowsAsync<KeyNotFoundException>(async () => await repository.Update(updateRental, 10111));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Contains("Rental not found..."));
        }

        [Test]
        public async Task UpdateRental_DbUpdateException()
        {
            var rental = new Rental
            {
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                Status = RentalStatus.Pending,
                RentalDate = DateTime.Now,
            };

            var updateRental = new Rental
            {
                ReturnDate = DateTime.Now,
                Status = RentalStatus.Returned,
            };
            await repository.Add(rental);
            var result = Assert.ThrowsAsync<DbUpdateException>(async () => await repository.Update(updateRental, 2));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Contains("Failed to update the rental due to a database issue"));
        }

        [Test]
        public async Task UpdateRental_Exception()
        {
            var rental = new Rental
            {
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                Status = RentalStatus.Pending,
                RentalDate = DateTime.Now,
            };

            var updateRental = new Rental
            {
                ReturnDate = DateTime.Now,
                Status = RentalStatus.Returned,
            };
            await repository.Add(rental);
            context.Dispose();
            var result = Assert.ThrowsAsync<Exception>(async () => await repository.Update(updateRental, 2));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Contains("Rental update failed due to an unexpected error"));
        }

        [Test]
        public async Task SaveChangesAsync()
        {
            await repository.SaveChangesAsync();

            Assert.Pass();
        }
    }
}
