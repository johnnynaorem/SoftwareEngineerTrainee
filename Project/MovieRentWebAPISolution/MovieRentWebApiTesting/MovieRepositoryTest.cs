using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
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
    public  class MovieRepositoryTest
    {
        MovieRentContext context;
        MovieRepository repository;
        DbContextOptions options;


        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            repository = new MovieRepository(context);
        }

        [Test]
        public async Task AddMovie_Test()
        {
            //Arrangement
            var movie = new Movie
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
            };

            var addedMovie = await repository.Add(movie);
            Assert.IsTrue(addedMovie.Title == movie.Title);
        }

        [Test]
        public async Task GetMovie_test()
        {
            var movie = new Movie
            {
                Title = "test",
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
            };

            var addedMovie = await repository.Add(movie);
            var getMovie = await repository.Get(1);
            Assert.IsNotNull(getMovie);
        }

        [Test]
        [TestCase("Test1", "test Genre1", "Test Description1", 8, 20, 200, "TestHashKey1")]
        [TestCase("Test2", "test Genre2", "Test Description2", 5, 10, 100, "TestHashKey2")]
        public async Task GetAllMovies_test(string Title, string Genre, string Description, double rating, int avilCopy, double rentalPrice, string image)
        {
            var movie = new Movie
            {
                Title = Title,
                Genre = Genre,
                Description = Description,
                Rating = rating,
                AvailableCopies = avilCopy,
                Rental_Price = rentalPrice,
                CoverImage = image
            };
            var addedMovie = await repository.Add(movie);

            var getMovies = await repository.GetAll();
            Assert.IsNotNull(getMovies);
        }
    }
}
