using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Exceptions;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyClaimTest
{
    public class ClaimRepositoryTest
    {
        DbContextOptions options;
        PolicyContext context;
        ClaimRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<PolicyContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new PolicyContext((DbContextOptions<PolicyContext>)options);
            repository = new ClaimRepository(context);
        }

        [Test]
        public async Task AddClaim_Test()
        {
            //Arrangement
            Claim claim = new Claim
            {
                ClaimantId = 1,
                PolicyNumber = "POL123",
                ClaimDate = DateTime.Now,
                TypeName = "Test",
            };

            var addedUser = await repository.Add(claim);
            Assert.NotNull(addedUser);

            Assert.AreEqual(1, addedUser.ClaimID);
        }

        [Test]
        public async Task GetClaim_Test()
        {
            //Arrangement
            Claim claim = new Claim
            {
                ClaimantId = 1,
                PolicyNumber = "POL123",
                ClaimDate = DateTime.Now,
                TypeName = "Test",
            };

            //Act
            await repository.Add(claim);
            var result = await repository.GetAll();

            //Assert
            Assert.IsNotNull(result);
        }

        //Exceptions Testing

        [Test]
        public async Task ClaimRepositoryAdd_CouldNotAddException()
        {
            Claim claim = new Claim
            {
                ClaimantId = 1,
                PolicyNumber = null,
                ClaimDate = DateTime.Now
            };

            var exception = Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(claim));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Fail to add Claim");
        }


        [Test]
        public async Task ClaimRepositoryGetAll_EmptyCollectionException()
        {
            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await repository.GetAll());
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Claims Collection Empty");
        }
    }

}
