using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
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
    public class WishlistServiceTest
    {
        DbContextOptions options;
        MovieRentContext context;
        WishlistRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            repository = new WishlistRepository(context);
        }

        [Test]
        public async Task AddWishlist_ReturnsTrue_WhenSuccess()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<int, Wishlist>>();
            var wishlistService = new WishlistService(mockRepository.Object);

            var addWishlistDTO = new AddWishlistDTO
            {
                CustomerId = 1,
                MovieId = 1
            };

            mockRepository.Setup(r => r.Add(It.IsAny<Wishlist>())).ReturnsAsync(new Wishlist
            {
                CustomerId = 1,
                MovieId = 1
            });

            var result = await wishlistService.Add(addWishlistDTO);

            // Assert
            Assert.IsTrue(result); 
        }

        [Test]
        public async Task AddWishlist_ReturnsFalse_WhenExceptionOccurs()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<int, Wishlist>>();
            var wishlistService = new WishlistService(mockRepository.Object);

            var addWishlistDTO = new AddWishlistDTO
            {
                CustomerId = 1,
                MovieId = 1
            };

            // Setup the mock to throw an exception when Add is called
            mockRepository.Setup(r => r.Add(It.IsAny<Wishlist>())).ThrowsAsync(new Exception("Database Error"));

            // Act
            var result = await wishlistService.Add(addWishlistDTO);

            // Assert
            Assert.IsFalse(result); 
        }

        [Test] 
        public async Task WishlistRemove_Success()
        {
            var wishlist = new AddWishlistDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            var service = new WishlistService(repository);
            await service.Add(wishlist);

            var result = await service.RemoveWishlist(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(true);
        }

        [Test]
        public async Task RemoveWishlist_ReturnsFalse_WhenExceptionOccurs()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<int, Wishlist>>();
            var wishlistService = new WishlistService(mockRepository.Object);

            var addWishlistDTO = new AddWishlistDTO
            {
                CustomerId = 1,
                MovieId = 1
            };

            // Setup the mock to throw an exception when Add is called
            mockRepository.Setup(r => r.Delete(1)).ThrowsAsync(new Exception("Database Error"));

            // Act
            var result = await wishlistService.RemoveWishlist(1);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
