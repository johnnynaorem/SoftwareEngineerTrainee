using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Repositories;
using MovieRentWebAPI.Services;
using NUnit.Framework;

namespace MovieRentWebApiTesting
{
    public class UserServiceTest
    {
        DbContextOptions options;
        MovieRentContext context;
        UserRepository repository;
        Mock<ILogger<UserService>> loggerUserService;
        Mock<TokenService> mockTokenService;
        Mock<IConfiguration> mockConfiguration;
        private Mock<IEmailSender> _mockEmailSender;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            repository = new UserRepository(context);
            loggerUserService = new Mock<ILogger<UserService>>();
            mockConfiguration = new Mock<IConfiguration>();
            mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            mockTokenService.Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>())).ReturnsAsync("TestToken");
            _mockEmailSender = new Mock<IEmailSender>();
        }

        [Test]

        [TestCase("TestUser2", "TestPassword2", "TestHashKey", UserRole.Admin)]
        public async Task CreateUser_Test(string username, string password, string hashKey, UserRole role)
        {
            var user = new UserCreateDTO
            {
                UserName = username,
                Password = password,
                Role = role
            };
            var userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object, _mockEmailSender.Object);
            var addedUser = await userService.CreateUser(user);
            Assert.IsTrue(addedUser.Username == user.UserName);
        }

        [Test]

        [TestCase("TestUser2", "TestPassword2", "TestHashKey", UserRole.Admin)]
        public async Task UserLogin_Test(string username, string password, string hashKey, UserRole role)
        {
            var user = new UserCreateDTO
            {
                UserName = username,
                Password = password,
                Role = role
            };
            var userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object, _mockEmailSender.Object);
            var addedUser = await userService.CreateUser(user);
            var loggedInUser = await userService.LoginUser(new LoginRequestDTO
            {
                UserEmail = user.UserEmail,
                Password = user.Password
            });
            Assert.IsTrue(addedUser.Username == loggedInUser.Username);
        }
    }
}
