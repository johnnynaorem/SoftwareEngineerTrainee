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
    public class ClaimantServiceTest
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
        public async Task ClaimantCreate_Test()
        {
            var claimant = new ClaimantDTO
            {
                Name = "Test",
                Phone = "72763487",
                Email = "john@gmail.com"
            };

            var expectedClaimant = new Claimant
            {
                ClaimantId = 1,
                Name = "Test",
                Phone = "72763487",
                Email = "john@gmail.com"
            };

            //var claimService = new ClaimService(repository);
            ClaimantService claimantService = new ClaimantService(repository);
            var addClaimant = await claimantService.CreateClaiment(claimant);

            Assert.IsNotNull(addClaimant);
            Assert.AreEqual(addClaimant, expectedClaimant.ClaimantId);
        }

        [Test]
        public async Task ClaimantGet_Test()
        {
            var claimant = new ClaimantDTO
            {
                Name = "Test",
                Phone = "72763487",
                Email = "john@gmail.com"
            };

            var expectedClaimant = new Claimant
            {
                ClaimantId = 1,
                Name = "Test",
                Phone = "72763487",
                Email = "john@gmail.com"
            };

            ClaimantService claimantService = new ClaimantService(repository);
            await claimantService.CreateClaiment(claimant);
            var result = await claimantService.GetAll();
            Assert.IsNotNull(result);
        }
    }
}
