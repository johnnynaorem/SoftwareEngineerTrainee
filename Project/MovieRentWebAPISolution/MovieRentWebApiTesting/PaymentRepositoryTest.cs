using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Repositories;
using NUnit.Framework;

namespace MovieRentWebApiTesting
{
    internal class PaymentRepositoryTest
    {
        DbContextOptions options;
        MovieRentContext context;
        PaymentRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("Test-Database")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            repository = new PaymentRepository(context);

            // Ensure the database is clean before each test
            // context.Database.EnsureDeleted();
            // context.Database.EnsureCreated();

        }

        [Test]
        public async Task AddPayment_Success()
        {
            var payment = new Payment
            {
                CustomerId = 1,
                RentalId = 1,
                Amount = 100,
                PaymentDate = DateTime.Now,
                PaymentType = "UPI"
            };

            var result = await repository.Add(payment);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.paymentId);
        }

        [Test]
        public async Task AddPayment_Exception()
        {
            var payment = new Payment
            {
                CustomerId = 1,
                RentalId = 1,
                Amount = 100,
                PaymentDate = DateTime.Now,
                PaymentType = "UPI"
            };

            context.Dispose();

            var exception = Assert.ThrowsAsync<CouldNotAddException>
                (async () => await repository.Add(payment));
            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task Get_Success()
        {
            var payment = new Payment
            {
                CustomerId = 1,
                RentalId = 1,
                Amount = 100,
                PaymentDate = DateTime.Now,
                PaymentType = "UPI"
            };

            await repository.Add(payment);
            var result = await repository.Get(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.paymentId);
        }

        [Test]
        public async Task GetAll_Success()
        {
            var payment = new Payment
            {
                CustomerId = 2,
                RentalId = 1,
                Amount = 100,
                PaymentDate = DateTime.Now,
                PaymentType = "UPI"
            };

            await repository.Add(payment);
            var result = await repository.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAll_EmptyCollectionException()
        {
            var _options = new DbContextOptionsBuilder<MovieRentContext>()
               .UseInMemoryDatabase("Test1")
               .Options;
            var _context = new MovieRentContext(_options);
            var _repository = new PaymentRepository(_context);

            var exception = Assert.ThrowsAsync<EmptyCollectionException>
                (async () => await _repository.GetAll());
            Assert.IsNotNull(exception);
            Assert.AreEqual("Payment Collection Empty", exception.Message);
        }

        [Test]
        public async Task Update_NotImplementedException()
        {
            var payment = new Payment
            {
                CustomerId = 2,
                RentalId = 1,
                Amount = 100,
                PaymentDate = DateTime.Now,
                PaymentType = "UPI"
            };
            var exception = Assert.ThrowsAsync<NotImplementedException>
                (async () => await repository.Update(payment, 3));

            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task Delete_NotImplementedException()
        {
            var exception = Assert.ThrowsAsync<NotImplementedException>
                (async () => await repository.Delete(3));

            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task SaveChangesAsync_Test()
        {
            var result =  repository.SaveChangesAsync();
            Assert.IsNotNull(result);
        }
    }
}
