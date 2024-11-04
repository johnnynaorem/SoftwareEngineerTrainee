using Microsoft.EntityFrameworkCore;
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
    public class ClaimantRepositoryTest
    {
        DbContextOptions options;
        PolicyContext context;
        ClaimantRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<PolicyContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new PolicyContext((DbContextOptions<PolicyContext>)options);
            repository = new ClaimantRepository(context);
        }

        [Test]
        public async Task AddClaimant_Test()
        {
            //Arrangement
            var claimant = new Claimant
            {
                Name = "Test",
                Phone ="8787470933",
                Email = "johnnynaorem@gmail.com"
            };

            var addClaimant = await repository.Add(claimant);
            Assert.NotNull(addClaimant);

            Assert.AreEqual(1, addClaimant.ClaimantId);
        }

        [Test]
        public async Task GetClaimant_Test()
        {
            //Arrangement
            var claimant = new Claimant
            {
                Name = "Test",
                Phone = "8787470933",
                Email = "johnnynaorem@gmail.com"
            };

            //Act
            await repository.Add(claimant);
            var result = await repository.GetAll();

            //Assert
            Assert.IsNotNull(result);
        }

        //Exceptions Testing

        [Test]
        public async Task ClaimantRepositoryAdd_CouldNotAddException()
        {
            var claimant = new Claimant
            {
                Name = null,
                Phone = "8787470933",
                Email = "johnnynaorem@gmail.com"
            };

            var exception = Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(claimant));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Fail to add Claimant");
        }


        [Test]
        public async Task ClaimantRepositoryGetAll_EmptyCollectionException()
        {
            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await repository.GetAll());
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Claimants Collection Empty");
        }
    }
}
