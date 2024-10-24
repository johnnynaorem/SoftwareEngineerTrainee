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
    public class PolicyRepositoryTest
    {
        DbContextOptions options;
        PolicyContext context;
        PolicyRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<PolicyContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new PolicyContext((DbContextOptions<PolicyContext>)options);
            repository = new PolicyRepository(context);
        }

        [Test]
        public async Task AddPolicy_Test()
        {
            //Arrangement
            Policy policy = new Policy
            {
                
                PolicyNumber = "POL123",
                AboutPolicy = "Something"
            };

            var addPolicy = await repository.Add(policy);
            Assert.NotNull(addPolicy);

            Assert.AreEqual(policy.PolicyNumber, addPolicy.PolicyNumber);
        }

        [Test]
        public async Task GetPolicy_Test()
        {
            //Arrangement
            Policy policy = new Policy
            {

                PolicyNumber = "POL123",
                AboutPolicy = "Something"
            };

            //Act
            await repository.Add(policy);
            var result = await repository.GetAll();

            //Assert
            Assert.IsNotNull(result);
        }

        //Exceptions Testing

        [Test]
        public async Task PolicyRepositoryAdd_CouldNotAddException()
        {
            Policy policy = new Policy
            {

                PolicyNumber = null,
                AboutPolicy = "Something"
            };

            var exception = Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(policy));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Fail to add Policy");
        }


        [Test]
        public async Task PolicyRepositoryGetAll_EmptyCollectionException()
        {
            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await repository.GetAll());
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Policies Collection Empty");
        }
    }

}
