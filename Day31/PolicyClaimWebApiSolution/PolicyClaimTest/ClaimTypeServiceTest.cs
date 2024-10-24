using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Repositories;
using PolicyClaimWebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolicyClaimWebApi.Interfaces;

namespace PolicyClaimTest
{
    public class ClaimTypeServiceTest
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
        public async Task ClaimTypeCreate_Test()
        {
            var claimType = new ClaimTypeDTO
            {
                PolicyNumber = "POL123",
                TypeName = "Something",
                TypeDescription = "Testing"
            };

            var expectedClaimType = new ClaimType
            {
                PolicyNumber = "POL123",
                TypeName = "Something",
                TypeDescription = "Testing"
            };

            ClaimTypeService claimTypeService = new ClaimTypeService(repository);
            var addClaimType = await claimTypeService.CreateClaimType(claimType);

            Assert.IsNotNull(addClaimType);
            Assert.AreEqual(addClaimType, "Something");
        }

        [Test]
        public async Task ClaimTypeGet_Test()
        {
            var claimType = new ClaimTypeDTO
            {
                PolicyNumber = "POL123",
                TypeName = "Something",
                TypeDescription = "Testing"
            };

            var expectedClaimType = new ClaimType
            {
                PolicyNumber = "POL123",
                TypeName = "Something",
                TypeDescription = "Testing"
            };

            ClaimTypeService claimTypeService = new ClaimTypeService(repository);
            await claimTypeService.CreateClaimType(claimType);

            var result = await claimTypeService.GetAll();
            Assert.IsNotNull(result);
        }
    }
}
