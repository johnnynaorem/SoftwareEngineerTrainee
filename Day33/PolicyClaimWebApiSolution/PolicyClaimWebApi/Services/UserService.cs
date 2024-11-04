using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace PolicyClaimWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<string, User> _userRepo;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<string, User> userRepository, ILogger<UserService> logger, ITokenService tokenService)
        {
            _userRepo = userRepository;
            _logger = logger;
            _tokenService = tokenService;
        }
        public async Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser)
        {
            var user = await _userRepo.Get(loginUser.Username);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            HMACSHA256 hmac = new HMACSHA256(user.HashKey);
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.Password[i])
                {
                    throw new Exception("Invalid username or password");
                }
            }
            return new LoginResponseDTO()
            {
                Username = user.Username,
                Token = await _tokenService.GenerateToken(new UserTokenDTO()
                {
                    Username = user.Username,
                    Role = user.Role.ToString()
                })

            };
        }

        public async Task<LoginResponseDTO> Register(UserDTO registerUser)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password));
            User user = new User()
            {
                Username = registerUser.Username,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = registerUser.Role
            };
            try
            {
                var addesUser = await _userRepo.Add(user);
                LoginResponseDTO response = new LoginResponseDTO()
                {
                    Username = user.Username
                };
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not register user");
                throw new Exception("Could not register user");
            }
        }
    }
}
