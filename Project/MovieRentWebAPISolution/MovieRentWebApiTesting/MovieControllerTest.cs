using AutoMapper;
using Microsoft.AspNetCore.Http;
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

namespace MovieRentWebApiTesting
{
    public class MovieControllerTest
    {
        private DbContextOptions<MovieRentContext> options;
        private MovieRepository repository;
        private MovieRentContext movieRentContext;
        private Mock<ILogger<MovieService>> loggerMovieService;
        private Mock<ILogger<MovieController>> loggerController;
        private IMovieService movieService;
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
            var resultObject = result as OkObjectResult;

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

        //Exceptions Test
        [Test]
        public async Task CreateUser_ReturnsInternalServerError_OnException()
        {
            var mockMovieService = new Mock<IMovieService>();
            var controller = new MovieController(mockMovieService.Object, loggerController.Object);

            var newMovie = new CreateMovieDTO
            {
                Title = "Echoes of the Past",
                Genre = "Mystery",
                Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                Rental_Price = 550,
                CoverImage = "movie.jpg",
                Rating = 8,
                AvailableCopies = 8,
            };

            mockMovieService.Setup(service => service.CreateMovie(newMovie))
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.AddMovie(newMovie) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(401, result.StatusCode);
        }

        [Test]
        public async Task MovieGetAll_InternalServerError500_Test()
        {
            var mockMovieService = new Mock<IMovieService>();
            var controller = new MovieController(mockMovieService.Object, loggerController.Object);

            var newMovie = new CreateMovieDTO
            {
                Title = "Echoes of the Past",
                Genre = "Mystery",
                Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                Rental_Price = 550,
                CoverImage = "movie.jpg",
                Rating = 8,
                AvailableCopies = 8,
            };

            mockMovieService.Setup(service => service.GetAll())
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.GetAllMovies() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async Task MovieFilter_InternalServerError500_Test()
        {
            var mockMovieService = new Mock<IMovieService>();
            var controller = new MovieController(mockMovieService.Object, loggerController.Object);

            var filter = new MovieFilterDTO
            {
                Title = "test",
                Genre = "test Genre",
            };

            mockMovieService.Setup(service => service.FilterMoviesAsync(filter))
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.FilterMovies(filter) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async Task MovieNamesGetAll_InternalServerError500_Test()
        {
            var mockMovieService = new Mock<IMovieService>();
            var controller = new MovieController(mockMovieService.Object, loggerController.Object);

            var newMovie = new CreateMovieDTO
            {
                Title = "Echoes of the Past",
                Genre = "Mystery",
                Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                Rental_Price = 550,
                CoverImage = "movie.jpg",
                Rating = 8,
                AvailableCopies = 8,
            };

            mockMovieService.Setup(service => service.GetAll())
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.GetAllMoviesNames() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async Task AddMovie_BadRequest400_Test()
        {
            var mockMovieService = new Mock<IMovieService>();
            var controller = new MovieController(mockMovieService.Object, loggerController.Object);
            controller.ModelState.AddModelError("Title", "Cannot be blank");

            var newMovie = new CreateMovieDTO
            {
                Title = null,
                Genre = "Mystery",
                Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                Rental_Price = 550,
                CoverImage = "movie.jpg",
                Rating = 8,
                AvailableCopies = 8,
            };

            mockMovieService.Setup(service => service.CreateMovie(newMovie))
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.AddMovie(newMovie) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Test]
        public async Task UpdateMovieDetails_Success200()
        {
            var _mockMovieService = new Mock<IMovieService>();
            var _controller = new MovieController(_mockMovieService.Object, loggerController.Object);

            var movie = new Movie
            {
                MovieId = 1,
                Title = "Echoes of the Past",
                Genre = "Mystery",
                Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                Rental_Price = 550,
                CoverImage = "movie.jpg",
                Rating = 8,
                AvailableCopies = 8,
            };

            var update = new CreateMovieDTO
            {
                Title = "Echoes of the Past Update",
                Genre = "Mystery Update",
            };

            _mockMovieService.Setup(s => s.UpdateMovie(update, 1))
                .ReturnsAsync(movie.MovieId);

            var result = await _controller.UpdateMovieDetails(update, 1) as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public async Task UpdateMovieDetails_InternalServerError500()
        {
            var _mockMovieService = new Mock<IMovieService>();
            var _controller = new MovieController(_mockMovieService.Object, loggerController.Object);

            var movie = new Movie
            {
                MovieId = 1,
                Title = "Echoes of the Past",
                Genre = "Mystery",
                Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                Rental_Price = 550,
                CoverImage = "movie.jpg",
                Rating = 8,
                AvailableCopies = 8,
            };

            var update = new CreateMovieDTO
            {
                Title = "Echoes of the Past Update",
                Genre = "Mystery Update",
            };

            _mockMovieService.Setup(s => s.UpdateMovie(update, 1))
                .ThrowsAsync(new Exception("Error in Server"));


            var result = await _controller.UpdateMovieDetails(update, 1) as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }

        [Test]
        public async Task GetAllCatalog_Success200_Ok()
        {
            var _mockMovieService = new Mock<IMovieService>();
            var _controller = new MovieController(_mockMovieService.Object, loggerController.Object);

            var movies = new List<Movie>()
            {
                new Movie
                {
                    MovieId = 1,
                    Title = "Echoes of the Past",
                    Genre = "Mystery",
                    Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                    Rental_Price = 550,
                    CoverImage = "movie.jpg",
                    Rating = 8,
                    AvailableCopies = 8,
                },
                new Movie
                {
                    MovieId = 2,
                    Title = "Echoes of the Past2",
                    Genre = "Mystery2",
                    Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                    Rental_Price = 550,
                    CoverImage = "movie.jpg",
                    Rating = 8,
                    AvailableCopies = 10,
                },
            };

            _mockMovieService.Setup(s => s.GetAll())
                .ReturnsAsync(movies);

            var result = await _controller.GetAllMoviesCatalog() as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public async Task GetAllCatalog_Exception_500()
        {
            var _mockMovieService = new Mock<IMovieService>();
            var _controller = new MovieController(_mockMovieService.Object, loggerController.Object);

            var movies = new List<Movie>()
            {
                new Movie
                {
                    MovieId = 1,
                    Title = "Echoes of the Past",
                    Genre = "Mystery",
                    Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                    Rental_Price = 550,
                    CoverImage = "movie.jpg",
                    Rating = 8,
                    AvailableCopies = 8,
                },
                new Movie
                {
                    MovieId = 2,
                    Title = "Echoes of the Past2",
                    Genre = "Mystery2",
                    Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                    Rental_Price = 550,
                    CoverImage = "movie.jpg",
                    Rating = 8,
                    AvailableCopies = 10,
                },
            };

            _mockMovieService.Setup(s => s.GetAll())
                .ThrowsAsync(new Exception("Error in Server"));

            var result = await _controller.GetAllMoviesCatalog() as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(result.StatusCode, 500);
        }


        [Test]
        public async Task GetMovie_Exception_500()
        {
            var _mockMovieService = new Mock<IMovieService>();
            var _controller = new MovieController(_mockMovieService.Object, loggerController.Object);

            
            _mockMovieService.Setup(s => s.GetMovie(1))
                .ThrowsAsync(new Exception("Error in Server"));

            var result = await _controller.GetMovie(1) as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
        }

        [Test]
        public async Task GetMovie_Success_200_OK()
        {
            var _mockMovieService = new Mock<IMovieService>();
            var _controller = new MovieController(_mockMovieService.Object, loggerController.Object);

            var movies = new List<Movie>()
            {
                new Movie
                {
                    MovieId = 1,
                    Title = "Echoes of the Past",
                    Genre = "Mystery",
                    Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                    Rental_Price = 550,
                    CoverImage = "movie.jpg",
                    Rating = 8,
                    AvailableCopies = 8,
                },
                new Movie
                {
                    MovieId = 2,
                    Title = "Echoes of the Past2",
                    Genre = "Mystery2",
                    Description = "A detective haunted by the unsolved murder of his sister returns to his hometown, only to uncover secrets that the townsfolk would rather keep buried. As he digs deeper, he realizes that the killer might be closer than he ever imagined",
                    Rental_Price = 550,
                    CoverImage = "movie.jpg",
                    Rating = 8,
                    AvailableCopies = 10,
                },
            };

            _mockMovieService.Setup(s => s.GetMovie(1))
                .ReturnsAsync(movies[1]);

            var result = await _controller.GetMovie(1) as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }

        [Test]
        public async Task GetAllMoviesWithPagination_Exception_500()
        {
            var _mockMovieService = new Mock<IMovieService>();
            var _controller = new MovieController(_mockMovieService.Object, loggerController.Object);


            _mockMovieService.Setup(s => s.GetAll(1,5))
                .ThrowsAsync(new Exception("Error in Server"));

            var result = await _controller.GetAllMovies(1,5) as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(result.StatusCode, 400);
        }

        [Test]
        public async Task GetAllMoviesWithPagination_Success_200_OK()
        {
            var _mockMovieService = new Mock<IMovieService>();
            var _controller = new MovieController(_mockMovieService.Object, loggerController.Object);
                      
            _mockMovieService.Setup(s => s.GetAll(1,5))
                .ReturnsAsync(new PaginatedResponseDTO<Movie>());

            var result = await _controller.GetAllMovies(1,5) as ObjectResult;
            Assert.NotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
        }
    }
}
