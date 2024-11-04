using AutoMapper;
using EFCoreWebAPI.Context;
using EFCoreWebAPI.Controllers;
using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTO;
using EFCoreWebAPI.Repositories;
using EFCoreWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreWebAPITest
{
    public class userControllerTesting
    {
        private DbContextOptions<ShoppingContext> options;
        private UserRepository repository;
        private ShoppingContext shoppingContext;
        private Mock<ILogger<UserRepository>> loggerUserRepo;
        private Mock<ILogger<UserService>> loggerUserService;
        private Mock<ILogger<AuthenticationController>> loggerController;
        private Mock<IMapper> mapper;
        private IUserService userService;
        private AuthenticationController userController;
        Mock<TokenService> mockTokenService;
        Mock<IConfiguration> mockConfiguration;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            shoppingContext = new ShoppingContext(options);
            loggerUserRepo = new Mock<ILogger<UserRepository>>();
            loggerUserService = new Mock<ILogger<UserService>>(); 
            loggerController = new Mock<ILogger<AuthenticationController>>();
            repository = new UserRepository(shoppingContext, loggerUserRepo.Object);
            mapper = new Mock<IMapper>();
            mockConfiguration = new Mock<IConfiguration>();
            mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            mockTokenService.Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>())).ReturnsAsync("TestToken");
            userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object);
            userController = new AuthenticationController(userService, loggerController.Object);
            mockConfiguration = new Mock<IConfiguration>();

        }

        [Test]
        [TestCase("TestUser1", "testPassword")]
        public async Task AddUserControllerTest(string username, string password)
        {
            // Arrange
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

           
            var newUser = new UserCreateDTO
            {
                Username = username,
                Password = password,
                Role = 0
            };

            var userEntity = new User
            {
                Username = username,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = 0
            };
            var userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object);

            mapper.Setup(m => m.Map<User>(newUser)).Returns(userEntity);
            var controller = new AuthenticationController(userService, loggerController.Object);

                // Act
                var result = await userController.CreateUser(newUser);
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
        [TestCase("TestUser1", "testPassword")]
        public async Task LoginIn_UserController_Testing(string username, string password)
        {
            // Arrange
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));


            var newUser = new UserCreateDTO
            {
                Username = username,
                Password = password,
                Role = 0
            };

            var userEntity = new User
            {
                Username = username,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = 0
            };


            var loginC = new LoginRequestDTO
            {
                Username = username,
                Password = password,
            };

            var loginR = new LoginResponseDTO
            {
                Username = username,
            };

            mapper.Setup(m => m.Map<User>(newUser)).Returns(userEntity);
            mapper.Setup(m => m.Map<LoginResponseDTO>(loginC)).Returns(loginR);
            var controller = new AuthenticationController(userService, loggerController.Object);

            // Act
            await userController.CreateUser(newUser);
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

        //Exceptions Testing
        [Test]
        public async Task CreateUser_ReturnsInternalServerError_OnException()
        {
            var mockUserService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<AuthenticationController>>();
            var controller = new AuthenticationController(mockUserService.Object, mockLogger.Object);

            var user = new UserCreateDTO
            {
                Username = "Something",
                Password = "password",
                Role = 0
            };

            mockUserService.Setup(service => service.Register(user))
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.CreateUser(user) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Test]
        [TestCase("TestUser1", "testPassword", "wrongPassword")]
        public async Task LoginUser_ReturnsUnauthorizeError_OnException(string username, string password, string wrongPass)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            var newUser = new UserCreateDTO
            {
                Username = username,
                Password = password,
                Role = 0
            };

            var userEntity = new User
            {
                Username = username,
                Password = passwordHash,
                HashKey = hmac.Key,
                Role = 0
            };


            var loginC = new LoginRequestDTO
            {
                Username = username,
                Password = wrongPass,
            };

            var loginR = new LoginResponseDTO
            {
                Username = username,
            };

            mapper.Setup(m => m.Map<User>(newUser)).Returns(userEntity);
            mapper.Setup(m => m.Map<LoginResponseDTO>(loginC)).Returns(loginR);

            var controller = new AuthenticationController(userService, loggerController.Object);


            // Act
            await controller.CreateUser(newUser);
            var result = await controller.LoginUser(loginC) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status401Unauthorized, result.StatusCode);
        }

    }
}
