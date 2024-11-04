using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;
using System.Security.Claims;

namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("UserRegistrarion")]

        public async Task<IActionResult> RegistrationUser(UserCreateDTO userCreateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.CreateUser(userCreateDTO);
                    return Ok(user);
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
                    ErrorMessage = ex.Message,
                });
            }
        }

        [HttpPost("UserLogin")]

        public async Task<IActionResult> LoginUser(LoginRequestDTO loginRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.LoginUser(loginRequest);
                    return Ok(user);
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

        [HttpPut("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDTO enitity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.ChangePassword(enitity, User);
                    return Ok(user);
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
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
