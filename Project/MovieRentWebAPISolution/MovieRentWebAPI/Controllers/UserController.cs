using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models.DTOs;
using System.Security.Claims;

namespace MovieRentWebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")]
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

        [HttpPost("UserRegistration")]

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
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser(string email)
        {
            try
                {
                var users = await _userService.GetUsers();
                var user = users.FirstOrDefault(u => u.UserEmail == email);
                return Ok(user);
                
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
