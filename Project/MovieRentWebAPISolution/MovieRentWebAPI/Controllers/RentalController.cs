using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ILogger<RentalController> _logger;

        public RentalController(IRentalService rentalService, ILogger<RentalController> logger)
        {
            _rentalService = rentalService;
            _logger = logger;
        }

        [HttpPost("RentMovie")]
        [Authorize(Roles = "user")]
        public async Task <IActionResult> RentMovie(RentMovieDTO rentMovie)
        {
            try
            {
                var response = await _rentalService.RentMovie(rentMovie);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = ex.Message,
                });
            }
            catch (MovieNotReservedException ex)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message,
                });
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpGet("GetRentals")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRentals()
        {
            try
            {
                var response = await _rentalService.GetRentals();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpPatch("UpdateRental")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRental(RentalUpdateRequestDTO rentMovie)
        {
            try
            {
                var response = await _rentalService.Update(rentMovie);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDTO
                {
                    ErrorCode = 500,
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
