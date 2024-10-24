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
    public class ClaimServiceTest
    {
        DbContextOptions options;
        PolicyContext context;
        ClaimRepository repository;
        ClaimFileRepository fileRepository;
        ClaimantRepository claimantRepository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<PolicyContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new PolicyContext((DbContextOptions<PolicyContext>)options);
            repository = new ClaimRepository(context);
            fileRepository = new ClaimFileRepository(context);
            claimantRepository = new ClaimantRepository(context);
        }

        [Test]
        public async Task ClaimCreate_Test()
        {
            var createDTO = new CreateClaimDTO
            {
                PolicyNumber = "POL123",
                ClaimantName = "test1",
                ClaimantEmail = "test1",
                ClaimantPhone = "34546",
                ClaimDate = DateTime.Now,
            };

            var expectedClaim = new Claim
            {
                PolicyNumber = "POL123",
                ClaimantId = 1,
                ClaimDate = DateTime.Now,
            };

            //var claimService = new ClaimService(repository);
            ClaimService claimService = new ClaimService(repository, fileRepository, claimantRepository);
            var addClaim = await claimService.CreateClaim(createDTO);

            Assert.IsNotNull(addClaim);
            Assert.AreEqual(addClaim.PolicyNumber, expectedClaim.PolicyNumber);
        }

        [Test]
        public async Task ClaimGet_Test()
        {
            var createDTO = new CreateClaimDTO
            {
                PolicyNumber = "POL123",
                ClaimantName = "test1",
                ClaimantEmail = "test1",
                ClaimantPhone = "34546",
                ClaimDate = DateTime.Now
            };

            var expectedClaim = new Claim
            {
                PolicyNumber = "POL123",
                ClaimantId = 1,
                ClaimDate = DateTime.Now,
            };

            ClaimService claimService = new ClaimService(repository, fileRepository, claimantRepository);
            await claimService.CreateClaim(createDTO);
            var result = await claimService.GetAll();
            Assert.IsNotNull(result);
        }


    }
}
