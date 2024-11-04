using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Repositories;
using PolicyClaimWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyClaimTest
{
    public class PolicyServiceTest
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
        public async Task PolicyCreate_Test()
        {
            var policy = new PolicyDTO
            {
                PolicyNumber = "POL123",
                AboutPolicy = "Something"
            };

            var expectedPolicy = new Policy
            {
                PolicyNumber = "POL123",
                AboutPolicy = "Something"
            };

            PolicyService policyService = new PolicyService(repository);
            var addPolicy = await policyService.CreatePolicy(policy);

            Assert.IsNotNull(addPolicy);
            Assert.AreEqual(addPolicy, expectedPolicy.PolicyNumber);
        }

        [Test]
        public async Task PolicyGet_Test()
        {
            var policy = new PolicyDTO
            {
                PolicyNumber = "POL123",
                AboutPolicy = "Something"
            };

            var expectedPolicy = new Policy
            {
                PolicyNumber = "POL123",
                AboutPolicy = "Something"
            };

            PolicyService policyService = new PolicyService(repository);
            await policyService.CreatePolicy(policy);

            var result = await policyService.GetAll();
            Assert.IsNotNull(result);
        }
    }
}
