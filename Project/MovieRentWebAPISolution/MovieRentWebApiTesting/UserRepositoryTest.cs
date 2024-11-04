using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentWebApiTesting
{
    public class UserRepositoryTest
    {
        DbContextOptions options;
        MovieRentContext context;
        UserRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new MovieRentContext((DbContextOptions<MovieRentContext>)options);
            repository = new UserRepository(context);
        }

        [Test]
        public async Task AddUser_Test()
        {
            //Arrangement
            var user = new User
            {
                UserEmail = "johnnynaorem7@gmail.com",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = UserRole.Admin
            };

            var addedUser = await repository.Add(user);
            Assert.IsTrue(addedUser.UserName == user.UserName);
        }

        [Test]
        public async Task GetUsers_test()
        {
            User user = new User
            {
                UserName = "TestUser",
                UserEmail = "johnnynaorem7@gmail.com",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = UserRole.Admin
            };
            var addedUser = await repository.Add(user);

            var getUser = await repository.Get("johnnynaorem7@gmail.com");
            Assert.IsNotNull(getUser);
        }

        [Test]
        [TestCase("TestUser1", "johnnynaorem7@gmail.com", "TestPassword1", "TestHashKey1", UserRole.Admin)]
        [TestCase("TestUser2", "johnnynao10@gmail.com", "TestPassword2", "TestHashKey2", UserRole.user)]
        public async Task GetAllUsers_test(string username, string email, string password, string hashKey, UserRole role)
        {
            User user = new User
            {
                UserName = username,
                UserEmail = email,
                Password = Encoding.UTF8.GetBytes(password),
                HashKey = Encoding.UTF8.GetBytes(hashKey),
                Role = role
            };
            var addedUser = await repository.Add(user);

            var getUser = await repository.GetAll();
            Assert.IsNotNull(getUser);
        }

        [Test]
        public async Task UpdateUser_test()
        {
            var user = new User
            {
                UserName = "TestUser",
                UserEmail = "johnnynaorem7@gmail.com",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = UserRole.Admin
            };

            var updateUser = new User
            {
                UserName = "TestUserUpdate",
                UserEmail = "johnnynaorem7@gmail.com",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = UserRole.Admin
            };
            await repository.Add(user);
            var update = await repository.Update(updateUser, user.UserEmail);
            Assert.IsNotNull(update);
            Assert.AreEqual(user.UserEmail, updateUser.UserEmail);
        }

        [Test]
        public async Task DeleteUser_test()
        {
            User user = new User
            {
                UserName = "TestUser",
                UserEmail = "johnnynaorem7@gmail.com",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = UserRole.Admin
            };
            var addUser = await repository.Add(user);
            var delete = await repository.Delete(addUser.UserEmail);
            Assert.IsNotNull(delete);
            Assert.AreEqual(user.UserEmail, delete.UserEmail);
        }

        [Test]
        public async Task DeleteUserFail_test()
        {
            var exception = Assert.ThrowsAsync<Exception>(async () => await repository.Delete("johnnynao10@gmail.com"));
            Assert.AreEqual(exception.Message, "Delete Failed");
        }

        [Test]
        public async Task GetAllUsers_ShouldThrowEmptyCollectionException_WhenNoUsersExist()
        {
            

            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await repository.GetAll());
            Assert.AreEqual("Users Collection Empty", exception.Message);
        }

        [Test]
        public async Task UpdateUserFail_test()
        {
            var exception = Assert.ThrowsAsync<CouldNotUpdateException>(async () => await repository.Update(new User
            {
                UserName = "TestUserUpdate",
                Role = UserRole.user,
            }, "johnnynao10@gmail.com"));
            Assert.AreEqual(exception.Message, "Update Failed");
        }
    }
}
