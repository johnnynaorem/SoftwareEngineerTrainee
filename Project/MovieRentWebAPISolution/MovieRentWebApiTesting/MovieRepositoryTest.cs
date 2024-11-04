using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Repositories;
using NUnit.Framework;

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

        [Test]
        public async Task UpdateMovie_Test()
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

            var updateMovie = new Movie
            {
                Title = "testUpdate",
                Rating = 8,
                AvailableCopies = 2,
                Rental_Price = 50,
                CoverImage = "Test Description Update",
                Genre = "test Genre update",
                Description = "Test Description update",
            };

            await repository.Add(movie);
            var updatedMovie = await repository.Update(updateMovie, movie.MovieId);
            Assert.IsTrue(updatedMovie.Title == "testUpdate");
        }

        [Test]
        public async Task MovieDelete_Test()
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

            await repository.Add(movie);
            var deletedMovie = await repository.Delete(movie.MovieId);

            Assert.IsNotNull(deletedMovie);
            Assert.AreEqual(movie.Title, deletedMovie.Title);
        }

        //Exceptions

        [Test]
        public async Task MovieAdd_CouldNotAddException_Test()
        {
            var movie = new Movie
            {
                Title = null,
                Genre = "test Genre",
                Description = "Test Description",
                Rating = 8,
                AvailableCopies = 10,
                Rental_Price = 100,
                CoverImage = "Test Description",
            };

            var exception = Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(movie));
            Assert.IsTrue(exception.Message.Contains("Movie Add Failed"));
        } 
        
        [Test]
        public async Task MovieGetAll_EmptyCollectionException_Test()
        {
            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await repository.GetAll());
            Assert.IsTrue(exception.Message.Contains("Movies Collection Empty"));
        }

        [Test]
        public async Task MovieDelete_Exception_Test()
        {
            var exception = Assert.ThrowsAsync<Exception>(async () => await repository.Delete(2));
            Assert.IsTrue(exception.Message.Contains("Delete Movie Failed"));
        }

        [Test]
        public async Task MovieUpdate_KeyNotFoundException_Test()
        {
            var updateMovie = new Movie
            {
                Title = "testUpdate",
                Rating = 8,
                AvailableCopies = 2,
                Rental_Price = 50,
                CoverImage = "Test Description Update",
                Genre = "test Genre update",
                Description = "Test Description update",
            };
            var exception =  Assert.ThrowsAsync<KeyNotFoundException>(async () => await repository.Update(updateMovie, 2));
            Assert.IsTrue(exception.Message.Contains("Movie with ID 2 not found."));
        }
    }
}
