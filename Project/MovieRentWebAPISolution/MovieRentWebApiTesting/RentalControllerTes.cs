using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Controllers;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentWebApiTesting
{
    internal class RentalControllerTes
    {
        private Mock<IRentalService> _mockRentalService;
        private Mock<ILogger<RentalController>> _mockLogger;
        private RentalController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockRentalService = new Mock<IRentalService>();
            _mockLogger = new Mock<ILogger<RentalController>>();
            _controller = new RentalController(_mockRentalService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetRental_success200Response()
        {
            var reantals = new List<Rental>
            {
                new Rental
                {
                    Status = RentalStatus.Active,
                    MovieId = 1,
                    CustomerId = 1,
                    RentalFee = 100,
                    RentalDate = DateTime.Now,
                }
            };

            _mockRentalService.Setup(s => s.GetRentals())
                .ReturnsAsync(reantals);

            var result = await _controller.GetRentals();

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task GetRental_500InternalServerErrror500_Response()
        {

            _mockRentalService.Setup(s => s.GetRentals())
                .ThrowsAsync(new Exception("Error"));

            var result = await _controller.GetRentals();

            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            var errorResponse = objectResult.Value as ErrorResponseDTO;
            Assert.IsNotNull(errorResponse);
            Assert.AreEqual(500, errorResponse.ErrorCode);
        }

        [Test]
        public async Task RentMovie_Success200_Response()
        {
            var rentMovie = new RentMovieDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            var rentEntity = new Rental
            {
                Status = RentalStatus.Active,
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                RentalDate = DateTime.Now,
            };

            _mockRentalService.Setup(s => s.RentMovie(rentMovie))
                .ReturnsAsync("OK");

            var result = await _controller.RentMovie(rentMovie);

            Assert.IsNotNull(result);
            var okResponse = result as OkObjectResult;
            Assert.IsNotNull(okResponse);

            Assert.AreEqual(200, okResponse.StatusCode);
        }

        [Test]
        public async Task RentMovie_InvalidOperationException_Response()
        {
            var rentMovie = new RentMovieDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            var rentEntity = new Rental
            {
                Status = RentalStatus.Active,
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                RentalDate = DateTime.Now,
            };

            _mockRentalService.Setup(s => s.RentMovie(rentMovie))
                .ThrowsAsync(new InvalidOperationException("Invalid Operator"));

            var result = await _controller.RentMovie(rentMovie);

            Assert.IsNotNull(result);
            var badResponse = result as BadRequestObjectResult;
            Assert.IsNotNull(badResponse);

            Assert.AreEqual(400, badResponse.StatusCode);
        }

        [Test]
        public async Task RentMovie_MovieNotReserveredException_Response()
        {
            var rentMovie = new RentMovieDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            var rentEntity = new Rental
            {
                Status = RentalStatus.Active,
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                RentalDate = DateTime.Now,
            };

            _mockRentalService.Setup(s => s.RentMovie(rentMovie))
                .ThrowsAsync(new MovieNotReservedException("Movie is not in reserved"));

            var result = await _controller.RentMovie(rentMovie);

            Assert.IsNotNull(result);
            var badResponse = result as BadRequestObjectResult;
            Assert.IsNotNull(badResponse);

            Assert.AreEqual(400, badResponse.StatusCode);
        }

        [Test]
        public async Task RentMovie_InternalServer500_Response()
        {
            var rentMovie = new RentMovieDTO
            {
                MovieId = 1,
                CustomerId = 1,
            };

            var rentEntity = new Rental
            {
                Status = RentalStatus.Active,
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                RentalDate = DateTime.Now,
            };

            _mockRentalService.Setup(s => s.RentMovie(rentMovie))
                .ThrowsAsync(new Exception("Internal Server Error"));

            var result = await _controller.RentMovie(rentMovie);

            Assert.IsNotNull(result);
            var badResponse = result as ObjectResult;
            Assert.IsNotNull(badResponse);

            Assert.AreEqual(500, badResponse.StatusCode);
        }


        [Test]
        public async Task UpdateRental_InternalServer500_Response()
        {
            var rentMovie = new RentalUpdateRequestDTO
            {
                Status = RentalStatus.Returned,
                ReturnDate = DateTime.Now,
            };


            _mockRentalService.Setup(s => s.Update(rentMovie))
                .ThrowsAsync(new Exception("Internal Server Error"));

            var result = await _controller.UpdateRental(rentMovie);

            Assert.IsNotNull(result);
            var badResponse = result as ObjectResult;
            Assert.IsNotNull(badResponse);

            Assert.AreEqual(500, badResponse.StatusCode);
        }

        [Test]
        public async Task UpdateRental_Success200Response_Response()
        {
            var rentMovie = new RentalUpdateRequestDTO
            {
                Status = RentalStatus.Returned,
                ReturnDate = DateTime.Now,
            };

            var rentEntity = new Rental
            {
                Status = RentalStatus.Returned,
                MovieId = 1,
                CustomerId = 1,
                RentalFee = 100,
                RentalDate = DateTime.Now,
            };

            _mockRentalService.Setup(s => s.Update(It.IsAny<RentalUpdateRequestDTO>()))
                .ReturnsAsync(rentEntity);

            var result = await _controller.UpdateRental(rentMovie);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}
