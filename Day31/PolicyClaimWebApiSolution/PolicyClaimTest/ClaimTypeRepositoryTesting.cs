using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Exceptions;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyClaimTest
{
    public class ClaimTypeRepositoryTesting
    {
        DbContextOptions options;
        PolicyContext context;
        ClaimTypeRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<PolicyContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new PolicyContext((DbContextOptions<PolicyContext>)options);
            repository = new ClaimTypeRepository(context);
        }

        [Test]
        public async Task AddClaimType_Test()
        {
            //Arrangement
            var claimType = new ClaimType
            {
                TypeDescription = "Something",
                PolicyNumber = "POL123",
                TypeName = "Test"
            };

            var addedClaimType = await repository.Add(claimType);
            Assert.NotNull(addedClaimType);

            Assert.AreEqual(claimType.PolicyNumber, addedClaimType.PolicyNumber);
        }

        [Test]
        public async Task GetClaimType_Test()
        {
            //Arrangement
            var claimType = new ClaimType
            {
                TypeDescription = "Something",
                PolicyNumber = "POL123",
                TypeName = "Test"
            };

            //Act
            await repository.Add(claimType);
            var result = await repository.GetAll();

            //Assert
            Assert.IsNotNull(result);
        }

        //Exceptions Testing

        [Test]
        public async Task ClaimTypeRepositoryAdd_CouldNotAddException()
        {
            var claimType = new ClaimType
            {
                TypeDescription = "Something",
                PolicyNumber = null,
                TypeName = "Test"
            };

            var exception = Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(claimType));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Fail to add ClaimType");
        }


        [Test]
        public async Task ClaimTypeRepositoryGetAll_EmptyCollectionException()
        {
            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await repository.GetAll());
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "ClaimType collection is empty");
        }
    }
}
