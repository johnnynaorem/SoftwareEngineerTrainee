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
using NUnit.Framework.Internal;
using System.Data;
using System.Security.Claims;
using System.Text;

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
        private ClaimsPrincipal userClaims;

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
            userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("Username", "TestUser2"),
            new Claim("Email", "test@example.com")
            }));
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

        [TestCase("TestUser1", "TestPassword1", "TestHashKey1", UserRole.Admin)]
        [TestCase("TestUser2", "TestPassword2", "TestHashKey2", UserRole.user)]
        public async Task GetAllUser_Test(string username, string password, string hashKey, UserRole role)
        {
            var user = new UserCreateDTO
            {
                UserName = username,
                Password = password,
                Role = role
            };
            var userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object, _mockEmailSender.Object);
            await userService.CreateUser(user);
            var results = await userService.GetUsers();

            Assert.IsNotNull(results);
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

        [Test]

        [TestCase("oldpassword", "newPassword")]
        public async Task UserChangePassword_Test(string oldpass, string newPass)
        {
            // Arrange
            var user = new UserCreateDTO
            {
                UserName = "TestUser2",
                UserEmail = "test@example.com",
                Password = "oldpassword",
                Role = UserRole.Admin
            };
            var userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object, _mockEmailSender.Object);
            await userService.CreateUser(user);

            var changePassword = new ChangePasswordRequestDTO
            {
                OldPassword = oldpass,
                NewPassword = newPass
            };

            var userPasswordUpdate = await userService.ChangePassword(changePassword, userClaims);
            
            Assert.IsNotNull(userPasswordUpdate);
            Assert.AreEqual(userPasswordUpdate.Username, user.UserName);
        }

        [Test]

        [TestCase("oldpassword", "newPassword")]
        public async Task UserChangePassword_UserNotFound_Test(string oldpass, string newPass)
        {
            // Arrange
            var user = new UserCreateDTO
            {
                UserName = "Test2",
                UserEmail = "test@example.com",
                Password = "oldpassword",
                Role = UserRole.Admin
            };
            var userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object, _mockEmailSender.Object);
            await userService.CreateUser(user);

            var changePassword = new ChangePasswordRequestDTO
            {
                OldPassword = oldpass,
                NewPassword = newPass
            };

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await userService.ChangePassword(changePassword, userClaims));

            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "User not found");
        }

        [Test]

        [TestCase("oldpassword", "newPassword")]
        public async Task UserChangePassword_OldPasswordIncorrect_Test(string oldpass, string newPass)
        {
            // Arrange
            var user = new UserCreateDTO
            {
                UserName = "TestUser2",
                UserEmail = "test@example.com",
                Password = "oldpassword",
                Role = UserRole.Admin
            };
            var userService = new UserService(repository, loggerUserService.Object, mockTokenService.Object, _mockEmailSender.Object);
            await userService.CreateUser(user);

            var changePassword = new ChangePasswordRequestDTO
            {
                OldPassword = "wrongPassword",
                NewPassword = newPass
            };

            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await userService.ChangePassword(changePassword, userClaims));

            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Incorrect old password");
        }
    }
}
