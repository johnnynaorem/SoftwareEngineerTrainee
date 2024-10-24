using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationController> _loggger;

        public AuthenticationController(IUserService userService, ILogger<AuthenticationController> logger)
        {
            _userService = userService;
            _loggger = logger;
        }

        [HttpPost("CreateUser")]
        public async Task <IActionResult> CreateUser(UserCreateDTO entity)
        {
            try
            {
                var newUser = await _userService.Register(entity);
                return Ok(newUser);
            }
            catch (Exception ex) 
            {
                _loggger.LogError(ex.Message);
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
                _loggger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
        }
    }
}
