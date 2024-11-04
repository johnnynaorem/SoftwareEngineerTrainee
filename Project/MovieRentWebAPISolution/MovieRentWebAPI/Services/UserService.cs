using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.EmailModels;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieRentWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<string, User> _userRepo;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        public UserService(IRepository<string, User> userRepository, ILogger<UserService> logger, ITokenService tokenService, IEmailSender emailSender)
        {
            _userRepo = userRepository;
            _logger = logger;
            _tokenService = tokenService;
            _emailSender = emailSender;
        }

        public async Task<LoginResponseDTO> ChangePassword(ChangePasswordRequestDTO entity, ClaimsPrincipal userClaims)
        {
            var userName = userClaims.Claims.FirstOrDefault(c => c.Type == "Username")?.Value;
            var userEmail = userClaims.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

            var users = await _userRepo.GetAll();

            var user = users.FirstOrDefault(u => u.UserName == userName && u.UserEmail == userEmail);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            HMACSHA256 hmac = new HMACSHA256(user.HashKey);
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(entity.OldPassword));

            if (!passwordHash.SequenceEqual(user.Password))
            {
                throw new Exception("Incorrect old password");
            }

            byte[] newPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(entity.NewPassword));
            user.Password = newPasswordHash;

            var userUpdateSuccess = await _userRepo.Update(user, user.UserEmail);
            if(userUpdateSuccess != null)
            {
                string body = $"Dear {userUpdateSuccess.UserName},\n\n" +
                            "Welcome to our service!\n\n" +
                            $"Your password has been successfully updated at {DateTime.Now}.\nHere is your new password:\n\n" +
                            $"**New Password:** {entity.NewPassword}\n\n" +
                            "For your security, please remember the following:\n" +
                            "- Do not share your password with anyone.\n" +
                            "- If you suspect any unauthorized access, please change your password immediately.\n\n" +
                            "Thank you for choosing us! If you have any questions, feel free to reach out to our support team.\n\n" +
                            "Best regards,\n" +
                            "The Support Team";
                SendMail("Update Password Successfully", body);
            }

            return new LoginResponseDTO()
            {
                Username = user.UserName,
                Token = ""
            };
        }

        private void SendMail(string title, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        "johnnynaorem7@gmail.com" },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }

        public async Task<LoginResponseDTO> CreateUser(UserCreateDTO userCreate)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userCreate.Password));
            User user = new User()
            {
                UserName = userCreate.UserName,
                Password = passwordHash,
                HashKey = hmac.Key,
                UserEmail = userCreate.UserEmail,
                Role = userCreate.Role
            };
            try
            {
                var addesUser = await _userRepo.Add(user);
                LoginResponseDTO response = new LoginResponseDTO()
                {
                    Username = addesUser.UserName,
                    Token = ""
                };
                if (addesUser != null)
                {
                    string body = $"Dear {userCreate.UserName},\n\n" +
                            "Welcome to our service!\n\n" +
                            "Your account has been successfully created. Here are your login details:\n\n" +
                            $"**Username:** {userCreate.UserEmail}\n" +
                            $"**Password:** {userCreate.Password}\n\n" +
                            "For your security, please remember the following:\n" +
                            "- Do not share your password with anyone.\n" +
                            "- We recommend changing your password after your first login to something more secure.\n" +
                            "- If you suspect any unauthorized access, please change your password immediately.\n\n" +
                            "Thank you for choosing us! If you have any questions, feel free to reach out to our support team.\n\n" +
                            "Best regards,\n" +
                            "The Support Team";
                    SendMail("Your Account Has Been Created", body);
                }
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not register user");
                throw new Exception("Could not register user");
            }
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDTO> LoginUser(LoginRequestDTO loginUser)
        {
            var user = await _userRepo.Get(loginUser.UserEmail);
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
                Username = user.UserName,
                Token = await _tokenService.GenerateToken(new UserTokenDTO()
                {
                    Username = user.UserName,
                    UserEmail = user.UserEmail,
                    Role = user.Role.ToString(),
                })
            };
        }
    }
}
