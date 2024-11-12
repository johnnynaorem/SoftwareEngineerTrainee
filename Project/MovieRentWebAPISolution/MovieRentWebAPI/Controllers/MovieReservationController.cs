using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieReservationController : ControllerBase
    {
        private readonly IReservationService _movieReservationService;
        private readonly ILogger<MovieReservationController> _logger;

        public MovieReservationController(IReservationService movieReservationService, ILogger<MovieReservationController> logger)
        {
            _movieReservationService = movieReservationService;
            _logger = logger;
        }

        [HttpPost("ReserveMovie")]
        [Authorize(Roles = "user")]

        public async Task <IActionResult> ReserveMovie(ReserveMovieDTO reserveMovie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var reservation = await _movieReservationService.ReserverMovie(reserveMovie);
                    return Ok(reservation);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpPatch("UpdateMovieReserveStatus")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateMovieReserveStatus(ReservedMovieStatusUpdateRequestDTO updateMovieReserveStatus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var reservation = await _movieReservationService.UpdateMovieReservationStatus(updateMovieReserveStatus);
                    return Ok(reservation);
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
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet("GetAllReservation")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllReservation()
        {
            try
            {
                    var reservation = await _movieReservationService.GetAll();
                    return Ok(reservation);
            }
            catch (EmptyCollectionException ex)
            {
                return Ok(new { message = "List is Empty",reservation = new List<Reservation>() });
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

        [HttpGet("GetReservationById")]
        [Authorize(Roles = "Admin, user")]

        public async Task<IActionResult> GetReservationById(int id)
        {
            try
            {
                var reservation = await _movieReservationService.GetReservationById(id);
                if (reservation == null)
                {
                    return Ok(reservation);
                }
                return Ok(new { message = $"Found Reservation with Id: {id}", reservation });
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

        [HttpGet("GetAllReservationByCustomer")]
        [Authorize]

        public async Task<IActionResult> GetAllReservationByCustomer(int customerId)
        {
            try
            {
                var reservation = await _movieReservationService.GetAll();
                var reservationByCustomer = (from reserve in reservation where reserve.CustomerId == customerId select new { reserve.ReservationId, reserve.MovieId, reserve.CustomerId, reserve.Status, reserve.ReservationDate }).ToList();
                return Ok(reservationByCustomer);
            }
            catch (EmptyCollectionException ex)
            {
                return Ok(new { message = "List is Empty", reservation = new List<Reservation>() });
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

        [HttpGet("GetAllReservationByMovie")]
        [Authorize]

        public async Task<IActionResult> GetAllReservationByMovie(int movieId)
        {
            try
            {
                var reservation = await _movieReservationService.GetAll();
                var reservationByCustomer = (from reserve in reservation where reserve.MovieId == movieId select new { reserve.ReservationId, reserve.MovieId, reserve.CustomerId, reserve.Status, reserve.ReservationDate }).ToList();
                return Ok(reservationByCustomer);
            }
            catch (EmptyCollectionException ex)
            {
                return Ok(new { message = "List is Empty", reservation = new List<Reservation>() });
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
