using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Controllers;
using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Repositories;
using MovieRentWebAPI.Services;
using NUnit.Framework;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieRentWebApiTesting
{
    public class UserControllerTest
    {
        private DbContextOptions<MovieRentContext> options;
        private UserRepository repository;
        private MovieRentContext movieRentContext;
        private Mock<ILogger<UserService>> loggerUserService;
        private Mock<ILogger<UserController>> loggerController;
        private IUserService userService;
        private UserController userController;
        Mock<TokenService> mockTokenService;
        Mock<IConfiguration> mockConfiguration;
        private Mock<IMapper> mapper;
        private Mock<IEmailSender> _mockEmailSender;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            movieRentContext = new MovieRentContext(options);
            loggerUserService = new Mock<ILogger<UserService>>();
            loggerController = new Mock<ILogger<UserController>>();
            repository = new UserRepository(movieRentContext);
            mockConfiguration = new Mock<IConfiguration>();
            mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            mockTokenService.Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>())).ReturnsAsync("TestToken");
            _mockEmailSender = new Mock<IEmailSender>();
            userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object, _mockEmailSender.Object);
            userController = new UserController(userService, loggerController.Object);
            mockConfiguration = new Mock<IConfiguration>();
            mapper = new Mock<IMapper>();

        }

        [Test]
        [TestCase("TestUser2", "TestPassword2", "TestHashKey", UserRole.Admin)]
        public async Task AddUserControllerTest(string username, string email, string password, UserRole role)
        {
            // Arrange
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));


            var newUser = new UserCreateDTO
            {
                UserName = username,
                UserEmail = email,
                Password = password,
                Role = role
            };

            var userEntity = new User
            {
                UserName = username,
                UserEmail = email,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = role,
            };

            mapper.Setup(m => m.Map<User>(newUser)).Returns(userEntity);

            // Act
            var result = await userController.RegistrationUser(newUser);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
            //var returnedUser = resultObject.Value as User;
            //Assert.IsNotNull(returnedUser);
            //Assert.AreEqual(username, returnedUser.Username);
        }

        [Test]
        [TestCase("TestUser2", "TestPassword2", "TestHashKey", UserRole.Admin)]
        public async Task LoginIn_UserController_Testing(string username, string email, string password, UserRole role)
        {
            // Arrange
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));


            var newUser = new UserCreateDTO
            {
                UserName = username,
                UserEmail = email,
                Password = password,
                Role = role
            };

            var userEntity = new User
            {
                UserName = username,
                UserEmail = email,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = role
            };


            var loginC = new LoginRequestDTO
            {
                UserEmail = email,
                Password = password,
            };

            var loginR = new LoginResponseDTO
            {
                Username = username
            };

            mapper.Setup(m => m.Map<LoginResponseDTO>(It.IsAny<LoginRequestDTO>())).Returns(loginR);
            userService.CreateUser(newUser); 

            // Act
            var result = await userController.LoginUser(loginC);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
            //var returnedUser = resultObject.Value as User;
            //Assert.IsNotNull(returnedUser);
            //Assert.AreEqual(username, returnedUser.Username);
        }

        [Test]
        [TestCase("TestUser2", "TestPassword2", "TestHashKey", UserRole.Admin, "newPassword")]
        public async Task ChangePassword_UserController_Testing(string username, string email, string password, UserRole role, string newPassword)
        {
            // Arrange
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));


            var newUser = new UserCreateDTO
            {
                UserName = username,
                UserEmail = email,
                Password = password,
                Role = role
            };

            var userEntity = new User
            {
                UserName = username,
                UserEmail = email,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = role
            };


            var updatePassword = new ChangePasswordRequestDTO
            {
                OldPassword = password,
                NewPassword = newPassword,
            };

            var userServiceMock = new Mock<IUserService>();
            var userController = new UserController(userServiceMock.Object, loggerController.Object);

            await userController.RegistrationUser(newUser);

            // Mocking the ClaimsPrincipal
            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Username", username),
                new Claim("Email", email)
            }));

            userController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

            // Act
            var result = await userController.ChangePassword(updatePassword);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }


        //Exceptions Testing
        [Test]
        public async Task CreateUser_ReturnsInternalServerError_OnException()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object, loggerController.Object);

            var user = new UserCreateDTO
            {
                UserName = "Something",
                UserEmail = "something@gmail.com",
                Password = "password",
                Role = 0
            };

            mockUserService.Setup(service => service.CreateUser(user))
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.RegistrationUser(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
        
        [Test]
        public async Task CreateUser_BadRequest_OnException()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object, loggerController.Object);
            controller.ModelState.AddModelError("UserEmail", "Enter valid email");

            var user = new UserCreateDTO
            {
                UserName = "Something",
                UserEmail = "something@gmail.com",
                Password = "password",
                Role = 0
            };


            // Act
            var result = await controller.RegistrationUser(user) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        [TestCase("test13", "test@gmail.com", "testPassword", "wrongPassword")]
        public async Task LoginUser_ReturnsUnauthorizeError_OnException(string username, string email, string password, string wrongPass)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            var newUser = new UserCreateDTO
            {
                UserName = username,
                UserEmail = email,
                Password = password,
                Role = 0
            };

            var userEntity = new User
            {
                UserName = username,
                UserEmail = email,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = 0
            };


            var loginC = new LoginRequestDTO
            {
                UserEmail = email,
                Password = wrongPass,
            };

            var loginR = new LoginResponseDTO
            {
                Username = username,
            };

            mapper.Setup(m => m.Map<User>(newUser)).Returns(userEntity);
            mapper.Setup(m => m.Map<LoginResponseDTO>(loginC)).Returns(loginR);


            // Act
            await userController.RegistrationUser(newUser);
            var result = await userController.LoginUser(loginC) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status401Unauthorized, result.StatusCode);
        }

        [Test]
        [TestCase("test13", "test@gmail.com", "testPassword", "wrongPassword")]
        public async Task UserChangePassword_ReturnsUnauthorizeError_OnException(string username, string email, string password, string wrongPass)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            var newUser = new UserCreateDTO
            {
                UserName = username,
                UserEmail = email,
                Password = password,
                Role = 0
            };

            var userEntity = new User
            {
                UserName = username,
                UserEmail = email,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = 0
            };


            var changePassword = new ChangePasswordRequestDTO
            {
                OldPassword = "wrongPassword",
                NewPassword = password,
            };


            mapper.Setup(m => m.Map<User>(newUser)).Returns(userEntity);


            // Act
            await userController.RegistrationUser(newUser);
            var result = await userController.ChangePassword(changePassword) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status401Unauthorized, result.StatusCode);
        }

        [Test]
        public async Task UserLogin_BadRequest_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object, loggerController.Object);
            controller.ModelState.AddModelError("UserEmail", "Enter valid email");

            var loginC = new LoginRequestDTO
            {
                Password = "password",
                UserEmail = "something@gmail.com"
            };


            // Act
            var result = await controller.LoginUser(loginC);
            Assert.NotNull(result);
            var BadRequest = result as BadRequestObjectResult;

            Assert.IsNotNull(BadRequest);
            Assert.AreEqual(400, BadRequest.StatusCode);
        }

        [Test]
        public async Task UserPasswordChange_BadRequest_Test()
        {
            var mockUserService = new Mock<IUserService>();
            var controller = new UserController(mockUserService.Object, loggerController.Object);
            controller.ModelState.AddModelError("OldPassword", "Old password is required");

            var loginC = new ChangePasswordRequestDTO
            {
                OldPassword = null,
                NewPassword = "newPassword"
            };


            // Act
            var result = await controller.ChangePassword(loginC);
            Assert.NotNull(result);
            var BadRequest = result as BadRequestObjectResult;

            Assert.IsNotNull(BadRequest);
            Assert.AreEqual(400, BadRequest.StatusCode);
        }
    }
}
