using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Controllers;
using MovieRentWebAPI.EmailInterface;
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
    public class MovieControllerTest
    {
        private DbContextOptions<MovieRentContext> options;
        private MovieRepository repository;
        private MovieRentContext movieRentContext;
        private Mock<ILogger<MovieService>> loggerMovieService;
        private Mock<ILogger<MovieController>> loggerController;
        private IMovieServive movieService;
        private MovieController movieController;
        private Mock<IMapper> mapper;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            movieRentContext = new MovieRentContext(options);
            loggerMovieService = new Mock<ILogger<MovieService>>();
            loggerController = new Mock<ILogger<MovieController>>();
            repository = new MovieRepository(movieRentContext);
            movieService = new MovieService(repository, loggerMovieService.Object);
            movieController = new MovieController(movieService, loggerController.Object);
            mapper = new Mock<IMapper>();
        }

        [Test]
        public async Task MovieCreate_Test()
        {
            //Arrangement
            var movie = new CreateMovieDTO
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };

            var movieEntity = new Movie
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };
            mapper.Setup(s => s.Map<Movie>(movie)).Returns(movieEntity);

            var result = movieController.AddMovie(movie);
            Assert.IsNotNull(result);
            var resultObject = await result as OkObjectResult;

            //// Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [Test]
        public async Task GetAllMovies_Test()
        {
            //Arrangement
            var movie = new CreateMovieDTO
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };

            var movieEntity = new Movie
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };
            mapper.Setup(s => s.Map<Movie>(movie)).Returns(movieEntity);

            await movieController.AddMovie(movie);
            var result = await movieController.GetAllMovies();
            Assert.IsNotNull(result);
            var resultObject =  result as OkObjectResult;

            //// Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [Test]
        public async Task GetAllMoviesName_Test()
        {
            //Arrangement
            var movie = new CreateMovieDTO
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };

            var movieEntity = new Movie
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };
            mapper.Setup(s => s.Map<Movie>(movie)).Returns(movieEntity);

            await movieController.AddMovie(movie);
            var result = await movieController.GetAllMoviesNames();
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;

            //// Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [Test]
        public async Task FilterMovies_Test()
        {
            //Arrangement

            var movie = new CreateMovieDTO
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };

            var movieEntity = new Movie
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };

            var filter = new MovieFilterDTO
            {
                Title = "test",
                Genre = "test Genre",
            };

            var filterEntity = new Movie
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
                ReleaseDate = DateTime.Now,
            };
            mapper.Setup(s => s.Map<Movie>(movie)).Returns(movieEntity);

            await movieController.AddMovie(movie);
            var result = await movieController.FilterMovies(filter);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;

            //// Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }
    }
}
