﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Interfaces;
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

        [HttpPost("MovieReservation")]
        [Authorize]

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

        [HttpPut("UpdateMovieReserveStatus")]
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
    }
}
