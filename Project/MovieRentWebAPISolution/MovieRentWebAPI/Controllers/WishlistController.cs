using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly ILogger<WishlistController> _logger;

        public WishlistController(IWishlistService wishlistService, ILogger<WishlistController> logger)
        {
            _wishlistService = wishlistService;
            _logger = logger;
        }

        [HttpPost("AddWishlist")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> AddWishlist(AddWishlistDTO wishlistDto)
        {
            try
            {
                var wishlist = await _wishlistService.Add(wishlistDto);
                return Ok(wishlist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpDelete("RemoveWishlist")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> RemoveWishlist(AddWishlistDTO wishlistDto)
        {
            try
            {
                var wishlist = await _wishlistService.RemoveWishlist(wishlistDto);
                return Ok(wishlist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponseDTO
                {
                    ErrorCode = 400,
                    ErrorMessage = ex.Message,
                });

            }
        }

        [HttpGet("GetWishlistByMovieIdAndCustomerId")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetWishlistByMovieIdAndCustomerId(int movieId, int customerId)
        {
            try
            {
                var wishlist = await _wishlistService.GetWishlistByMovieIdAndCustomerId(movieId, customerId);
                return Ok(wishlist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new ErrorResponseDTO
                {
                    ErrorCode = 204,
                    ErrorMessage = ex.Message,
                });

            }
        }

        [HttpGet("GetWishlistsByCustomer")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetWishlistsByCustomer(int customerId)
        {
            try
            {
                var wishlist = await _wishlistService.GetWishlistsByCustomer(customerId);
                return Ok(wishlist);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new ErrorResponseDTO
                {
                    ErrorCode = 204,
                    ErrorMessage = ex.Message,
                });

            }
        }
    }
}
