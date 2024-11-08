using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Controllers;
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
    public class WishlistControllerTest
    {
        private DbContextOptions<MovieRentContext> options;
        private MovieRentContext movieRentContext;
        private WishlistRepository wishlistRepository;
        //private Mock<ICustomerService> _mockCustomerService;
        private WishlistController wishlistController;
        private Mock<ILogger<WishlistController>> _mockLogger;
        private IWishlistService wishlistService;
        private Mock<IMapper> mapper;

        [SetUp]
        public void SetUp()
        {

            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            movieRentContext = new MovieRentContext(options);
            wishlistRepository = new WishlistRepository(movieRentContext);
            wishlistService = new WishlistService(wishlistRepository);
            _mockLogger = new Mock<ILogger<WishlistController>>();
            wishlistController = new WishlistController(wishlistService, _mockLogger.Object);
            mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task AddWishlist_Success200Response_Test()
        {
            var wishlist = new AddWishlistDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            var entityWishlist = new Wishlist
            {
                MovieId = 1,
                CustomerId = 1,
                WishlistId = 1,
            };

            mapper.Setup(s => s.Map<Wishlist>(wishlist)).Returns(entityWishlist);

            var result = await wishlistController.AddWishlist(wishlist);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

        [Test]
        public async Task AddWishlist_BadResponse400_Test()
        {
            var _mockWishlistService = new Mock<IWishlistService>();
            var controller = new WishlistController(_mockWishlistService.Object, _mockLogger.Object);

            var wishlist = new AddWishlistDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            _mockWishlistService.Setup(s => s.Add(wishlist)).ThrowsAsync(new Exception("Bad Request"));
            
            var result = await controller.AddWishlist(wishlist);

            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
        }

        [Test]
        public async Task RemoveWishlist_Success200Response_Test()
        {
            var wishlist = new AddWishlistDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            var entityWishlist = new Wishlist
            {
                MovieId = 1,
                CustomerId = 1,
                WishlistId = 1,
            };

            mapper.Setup(s => s.Map<Wishlist>(wishlist)).Returns(entityWishlist);
            await wishlistController.AddWishlist(wishlist);

            var result = await wishlistController.RemoveWishlist(wishlist);
            Assert.IsNotNull(result);
            var okResponse = result as ObjectResult;
            Assert.IsNotNull(okResponse);
            Assert.AreEqual(200, okResponse.StatusCode);
        }

        [Test]
        public async Task RemoveWishlist_BadResponse400_Test()
        {
            var _mockWishlistService = new Mock<IWishlistService>();
            var controller = new WishlistController(_mockWishlistService.Object, _mockLogger.Object);

            var wishlist = new AddWishlistDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            _mockWishlistService.Setup(s => s.RemoveWishlist(wishlist)).ThrowsAsync(new Exception("Bad Request"));

            var result = await controller.RemoveWishlist(wishlist);

            Assert.IsNotNull(result);
            var badRequest = result as ObjectResult;
            Assert.IsNotNull(badRequest);
            Assert.AreEqual(400, badRequest.StatusCode);
        }
    }
}
