using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;
using System.Data;

namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieServive, ILogger<MovieController> logger)
        {
            _logger = logger;
            _movieService = movieServive;
        }

        [HttpPost("AddMovie")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie(CreateMovieDTO movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var addMovie = await _movieService.CreateMovie(movie);
                    return Ok(addMovie);
                }
                else
                {
                    return BadRequest(new ErrorResponseDTO
                    {
                        ErrorCode = 400,
                        ErrorMessage = "one or more fields validate error"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ErrorResponseDTO
                {
                    ErrorCode = 401,
                    ErrorMessage = "An unexpected error occurred. Please try again later."
                });
            }
        }

        [HttpGet("GetAllMovies")]
        [Authorize]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _movieService.GetAll();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred. Please try again later."
                });
            }
        }

        [HttpGet("GetAllMoviesNames")]
        [Authorize]
        public async Task<IActionResult> GetAllMoviesNames()
        {
            try
            {
                var movies = await _movieService.GetAll();
                var moviesName = (from movie in movies
                                  orderby movie.Title
                                  select movie.Title);
                return Ok(moviesName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred. Please try again later."
                });
            }
        }

        [HttpGet("GetAllMoviesCatalog")]
        [Authorize]
        public async Task<IActionResult> GetAllMoviesCatalog()
        {
            try
            {
                var movies = await _movieService.GetAll();
                var moviesCategories = (from movie in movies
                                  orderby movie.Genre
                                  select movie.Genre).Distinct().ToList();
                return Ok(moviesCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = "An unexpected error occurred. Please try again later."
                });
            }
        }

        [HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> FilterMovies([FromQuery] MovieFilterDTO filter)
        {
            try
            {
                var movies = await _movieService.FilterMoviesAsync(filter);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet("GetMovieById")]
        [Authorize(Roles = "Admin,user")]
        public async Task<IActionResult> GetMovie(int id)
        {
            try
            {
                var result = await _movieService.GetMovie(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetMovies")]
        [Authorize(Roles = "Admin,user")]
        public async Task<IActionResult> GetAllMovies(int pageNumber = 1, int pageSize = 10 )
        {
            try
            {
                var result = await _movieService.GetAll(pageNumber, pageSize);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("UpdateMovieDetails")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovieDetails(CreateMovieDTO updateMovie, int movieId)
        {
            try
            {
                var movies = await _movieService.UpdateMovie(updateMovie, movieId);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
