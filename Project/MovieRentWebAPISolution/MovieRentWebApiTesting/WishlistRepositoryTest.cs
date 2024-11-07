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
    public class WishlistRepositoryTest
    {
        MovieRentContext context;
        WishlistRepository repository;
        DbContextOptions options;


        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            repository = new WishlistRepository(context);

        }

        [Test]
        public async Task WishlistAdd_Test()
        {
            var wishlist = new Wishlist
            {
                CustomerId = 1,
                MovieId = 1,
            };

            var result = await repository.Add(wishlist);
            Assert.IsNotNull(result);
            Assert.IsTrue(true);
        }

        [Test]
        public async Task WishlistRemove_Test()
        {

            var wishlist = new Wishlist
            {
                CustomerId = 3,
                MovieId = 3,
            };
            await repository.Add(wishlist);
            var result = await repository.Delete(1);
            Assert.IsNotNull(result);
            Assert.IsTrue(true);
        }

        [Test]
        public async Task WishlistGet_Test()
        {
            await WishlistAdd_Test();
            var result = await repository.Get(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.WishlistId, 1);
        }

        [Test]
        public async Task WishlistGetAll_Test()
        {
            var wishlist = new Wishlist
            {
                CustomerId = 2,
                MovieId = 2,
            };

            await repository.Add(wishlist);
            var result = await repository.GetAll();
            Assert.IsNotNull(result.Any());
        }

        [Test]
        public async Task UpdateWishlist_Test()
        {
            var update = new Wishlist
            {
                CustomerId = 1
            };
            var exception = Assert.ThrowsAsync<NotImplementedException>(async () => await repository.Update(update, 1));
            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task SaveChangesAsync_Test()
        {
            var exception = Assert.ThrowsAsync<NotImplementedException>(async () => await repository.SaveChangesAsync());
            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task EmptyException_Test()
        {
            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await repository.GetAll());
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Wishlists Collection Empty");
        }

        [Test]
        public async Task AddException_AlreadyExist_Test()
        {

            var wishlist = new Wishlist
            {
                CustomerId = 1,
                MovieId = 1,
            };

            await repository.Add(wishlist);

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await WishlistAdd_Test());
            Assert.IsNotNull(exception);
        }

        [Test]
        public async Task DeleteException_Test()
        {
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await repository.Delete(1));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Wishlist Not Found");
        }
    }
}
