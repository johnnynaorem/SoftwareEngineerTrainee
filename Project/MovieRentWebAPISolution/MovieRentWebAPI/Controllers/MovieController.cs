using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;

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
    }
}
