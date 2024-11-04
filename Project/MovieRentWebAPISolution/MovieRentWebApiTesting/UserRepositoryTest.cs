using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

            var getUser = await repository.Get(1);
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
    }
}
