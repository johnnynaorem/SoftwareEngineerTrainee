using Microsoft.EntityFrameworkCore;
using Moq;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.InterfaceForEmail;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Repositories;
using PolicyClaimWebApi.Services;


namespace PolicyClaimTest
{
    public class ClaimServiceTest
    {
        DbContextOptions options;
        PolicyContext context;
        ClaimRepository repository;
        ClaimFileRepository fileRepository;
        ClaimantRepository claimantRepository;
        private Mock<IEmailSender> _mockEmailSender;

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
            _mockEmailSender = new Mock<IEmailSender>();
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
            ClaimService claimService = new ClaimService(repository, fileRepository, claimantRepository, _mockEmailSender.Object);
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

            ClaimService claimService = new ClaimService(repository, fileRepository, claimantRepository, _mockEmailSender.Object);
            await claimService.CreateClaim(createDTO);
            var result = await claimService.GetAll();
            Assert.IsNotNull(result);
        }


    }
}
