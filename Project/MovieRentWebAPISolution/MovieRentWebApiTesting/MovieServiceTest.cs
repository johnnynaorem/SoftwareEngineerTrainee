using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Repositories;
using MovieRentWebAPI.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRentWebAPI.Interfaces;

namespace MovieRentWebApiTesting
{
    public class MovieServiceTest
    {
        DbContextOptions options;
        MovieRentContext context;
        MovieRepository repository;
        Mock<ILogger<MovieService>> loggerMovieService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            repository = new MovieRepository(context);
            loggerMovieService = new Mock<ILogger<MovieService>>();
        }

        [Test]
        public async Task CreateMovie_Test()
        {
            var movie = new CreateMovieDTO()
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
            };
            var MovieService = new MovieService(repository, loggerMovieService.Object);
            var addedMovie = await MovieService.CreateMovie(movie);
            Assert.IsNotNull(addedMovie);
            Assert.AreEqual(1, addedMovie);
        }

        [Test]
        [TestCase("Test1", "test Genre1", "Test Description1", 8, 20, 200, "TestHashKey1")]
        [TestCase("Test2", "test Genre2", "Test Description2", 5, 10, 100, "TestHashKey2")]
        public async Task GetAllMovies_test(string Title, string Genre, string Description, double rating, int avilCopy, double rentalPrice, string image)
        {
            var movie = new CreateMovieDTO
            {
                Title = Title,
                Genre = Genre,
                Description = Description,
                Rating = rating,
                AvailableCopies = avilCopy,
                Rental_Price = rentalPrice,
                CoverImage = image
            };
            var MovieService = new MovieService(repository, loggerMovieService.Object);
            var addedMovie = await MovieService.CreateMovie(movie);

            var results = await MovieService.GetAll();
            Assert.IsNotNull(results);
        }

        [Test]
        [TestCase("Test1", "test Genre1", "Test Description1", 8, 20, 200, "TestHashKey1")]
        [TestCase("Test2", "test Genre2", "Test Description2", 5, 10, 100, "TestHashKey2")]
        public async Task GetMovie_test(string Title, string Genre, string Description, double rating, int avilCopy, double rentalPrice, string image)
        {
            var movie = new CreateMovieDTO
            {
                Title = Title,
                Genre = Genre,
                Description = Description,
                Rating = rating,
                AvailableCopies = avilCopy,
                Rental_Price = rentalPrice,
                CoverImage = image
            };
            var MovieService = new MovieService(repository, loggerMovieService.Object);
            var addedMovie = await MovieService.CreateMovie(movie);

            var result = await MovieService.GetMovie(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test1", result.Title);
        }

        [Test]
        [TestCase("Test1", "test Genre1", "Test Description1", 8, 20, 200, "TestHashKey1")]
        [TestCase("Test2", "test Genre2", "Test Description2", 5, 10, 100, "TestHashKey2")]
        public async Task GetMovieByFilter_test(string Title, string Genre, string Description, double rating, int avilCopy, double rentalPrice, string image)
        {
            var movie = new CreateMovieDTO
            {
                Title = Title,
                Genre = Genre,
                Description = Description,
                Rating = rating,
                AvailableCopies = avilCopy,
                Rental_Price = rentalPrice,
                CoverImage = image
            };

            var filterDTO = new MovieFilterDTO
            {
                Title = Title,
                Genre = Genre,
                MinimumRating = rating,
                ReleaseDate = DateTime.Now,
            };

            var MovieService = new MovieService(repository, loggerMovieService.Object);
            await MovieService.CreateMovie(movie);


            var result = await MovieService.FilterMoviesAsync(filterDTO);
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase("Test1", "test Genre1", "Test Description1", 8, 20, 200, "TestHashKey1")]
        public async Task UpdateMovie_test(string Title, string Genre, string Description, double rating, int avilCopy, double rentalPrice, string image)
        {
            var movie = new CreateMovieDTO
            {
                Title = Title,
                Genre = Genre,
                Description = Description,
                Rating = rating,
                AvailableCopies = avilCopy,
                Rental_Price = rentalPrice,
                CoverImage = image
            };

            var updateMovieDTO = new CreateMovieDTO
            {
                Title = "Hello",
                Genre = "Genre",
                Description = "Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 250,
                CoverImage = "image.jpg"
            };

            var MovieService = new MovieService(repository, loggerMovieService.Object);
            await MovieService.CreateMovie(movie);


            var result = await MovieService.UpdateMovie(updateMovieDTO, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 1);
        }

        [Test]
        [TestCase("Test1", "test Genre1", "Test Description1", 8, 20, 200, "TestHashKey1")]
        public async Task UpdateMovie_Fail_test(string Title, string Genre, string Description, double rating, int avilCopy, double rentalPrice, string image)
        {
            
            var updateMovieDTO = new CreateMovieDTO
            {
                Title = Title,
                Genre = Genre,
                Description = Description,
                Rating = rating,
                AvailableCopies = avilCopy,
                Rental_Price = rentalPrice,
                CoverImage = image
            };

            var MovieService = new MovieService(repository, loggerMovieService.Object);
            var exception = Assert.ThrowsAsync< InvalidOperationException>(async () => await MovieService.UpdateMovie(updateMovieDTO, 1));
            Assert.IsNotNull(exception);

            Assert.IsTrue(exception.Message == "Update Failed");
        }
    }
}
