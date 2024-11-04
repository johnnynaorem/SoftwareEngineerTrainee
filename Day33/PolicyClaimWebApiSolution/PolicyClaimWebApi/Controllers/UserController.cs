using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models.DTOs;

namespace PolicyClaimWebApi.Controllers
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
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDTO entity)
        {
            try
            {
                var newUser = await _userService.Register(entity);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginRequestDTO entity)
        {
            try
            {
                var newUser = await _userService.Autheticate(entity);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
        }
    }
}
