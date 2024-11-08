using Castle.Core.Logging;
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
    internal class MovieRerservationControllerTest
    {
        private Mock<IReservationService> reservationServiceMock;
        private Mock<ILogger<MovieReservationController>> _mockLogger;
        private MovieReservationController _controller;

        [SetUp]
        public void Setup()
        {
            reservationServiceMock = new Mock<IReservationService>();
            _mockLogger = new Mock<ILogger<MovieReservationController>>();
            _controller = new MovieReservationController(reservationServiceMock.Object, _mockLogger.Object);
        }

        [Test]
        public async Task ReserveMovie_Success200Return_test()
        {
            var reserveMovieDto = new ReserveMovieDTO
            {
                CustomerId = 1,
                MovieId = 1,
                ReservationDate = DateTime.Now,
            };

            reservationServiceMock.Setup(s => s.ReserverMovie(It.IsAny<ReserveMovieDTO>()))
                .ReturnsAsync(1);

            var result = await _controller.ReserveMovie(reserveMovieDto);
            Assert.IsNotNull(result);
            var OkResponse = result as OkObjectResult;
            Assert.IsNotNull(OkResponse);
            Assert.AreEqual(200, OkResponse.StatusCode);
        }

        [Test]
        public async Task ReserveMovie_InternalServerError_500Return_test()
        {
            var reserveMovieDto = new ReserveMovieDTO
            {
                CustomerId = 1,
                MovieId = 1,
                ReservationDate = DateTime.Now,
            };

            reservationServiceMock.Setup(s => s.ReserverMovie(reserveMovieDto))
                .ThrowsAsync(new Exception("Internal Server Error"));

            var result = await _controller.ReserveMovie(reserveMovieDto);
            Assert.IsNotNull(result);
            var badResponse = result as ObjectResult;
            Assert.IsNotNull(badResponse);
            Assert.AreEqual(500, badResponse.StatusCode);
        }

        [Test]
        public async Task ReserveMovie_BadRequest_400Return_test()
        {
            _controller.ModelState.AddModelError("CustomerId", "Enter valid CustomerId");
            var reserveMovieDto = new ReserveMovieDTO
            {
                MovieId = 1,
                ReservationDate = DateTime.Now,
            };


            var result = await _controller.ReserveMovie(reserveMovieDto);
            Assert.IsNotNull(result);
            var badResponse = result as BadRequestObjectResult;
            Assert.IsNotNull(badResponse);
            Assert.AreEqual(400, badResponse.StatusCode);
        }

        [Test]
        public async Task GetAllReservation_Success_200_Response()
        {
            var reservations = new List<Reservation>()
            {
                new Reservation
                {
                    ReservationId = 1,
                    CustomerId = 1,
                    MovieId = 1,
                    ReservationDate = DateTime.Now,
                },
                new Reservation
                {
                    ReservationId = 2,
                    CustomerId = 2,
                    MovieId = 2,
                    ReservationDate = DateTime.Now,
                }
            };

            reservationServiceMock.Setup(s => s.GetAll())
                .ReturnsAsync(reservations);

            var result = await _controller.GetAllReservation();
            Assert.IsNotNull(result);
            var okResponse = result as OkObjectResult;
            Assert.IsNotNull(okResponse);
            Assert.AreEqual(200, okResponse.StatusCode);
        }

        [Test]
        public async Task GetAllReservation_InternalServerError_500()
        {

            reservationServiceMock.Setup(s => s.GetAll())
                .ThrowsAsync(new Exception("Internal Server Error"));

            var result = await _controller.GetAllReservation();
            Assert.IsNotNull(result);
            var badRequestResult = result as ObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(500, badRequestResult.StatusCode);
        }

        [Test]
        public async Task GetAllReservation_EmptyCollectionException_Test()
        {

            reservationServiceMock.Setup(s => s.GetAll())
                .ThrowsAsync(new EmptyCollectionException("Empty"));

            var result = await _controller.GetAllReservation();
            Assert.IsNotNull(result);
            var resultObject = result as ObjectResult;
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [Test]
        public async Task UpdateMovieReserveStatus_200_SuccessResponse()
        {
            var status = new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId = 1,
                Status = ReservationStatus.Active,
            };

            var statusUpdate = new ReservedStatusUpdateResponseDTO
            {
                ReservationId = 1,
                Status = "Active",
            };

            reservationServiceMock.Setup(s => s.UpdateMovieReservationStatus(status))
                .ReturnsAsync(statusUpdate);

            var result = await _controller.UpdateMovieReserveStatus(status);
            Assert.IsNotNull(result);
            var resultObject = result as ObjectResult;
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(resultObject.StatusCode, 200);
        }

        [Test]
        public async Task UpdateMovieReserveStatus_400_BadResponse()
        {
            _controller.ModelState.AddModelError("ReservationId", "cannot blank");
            var status = new ReservedMovieStatusUpdateRequestDTO
            {
                Status = ReservationStatus.Active,
            };

            var statusUpdate = new ReservedStatusUpdateResponseDTO
            {
                ReservationId = 1,
                Status = "Active",
            };

            reservationServiceMock.Setup(s => s.UpdateMovieReservationStatus(status))
                .ReturnsAsync(statusUpdate);

            var result = await _controller.UpdateMovieReserveStatus(status);
            Assert.IsNotNull(result);
            var resultObject = result as ObjectResult;
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(resultObject.StatusCode, 400);
        }

        [Test]
        public async Task UpdateMovieReserveStatus_500_InternalServerErrorResponse()
        {
            var status = new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId= 1,
                Status = ReservationStatus.Active,
            };

            reservationServiceMock.Setup(s => s.UpdateMovieReservationStatus(status))
                .ThrowsAsync(new Exception("Something occur"));

            var result = await _controller.UpdateMovieReserveStatus(status);
            Assert.IsNotNull(result);
            var resultObject = result as ObjectResult;
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(resultObject.StatusCode, 500);
        }
    }
}
