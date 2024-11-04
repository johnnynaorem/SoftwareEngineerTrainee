using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Controllers;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Repositories;
using PolicyClaimWebApi.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PolicyClaimTest
{
    public class ClaimControllerTest
    {
        private DbContextOptions<PolicyContext> options;
        private PolicyContext context;
        private ClaimRepository repository;
        private ClaimFileRepository fileRepository;
        private ClaimController claimController;
        private Mock<IClaimService> claimServiceMock;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<PolicyContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new PolicyContext(options);
            repository = new ClaimRepository(context);
            fileRepository = new ClaimFileRepository(context);
            claimServiceMock = new Mock<IClaimService>();
            claimController = new ClaimController(claimServiceMock.Object);
        }

        [Test]
        public async Task CreateClaimWithFiles_ValidRequest_ReturnsOk()
        {
            // Arrange
            var createDTO = new CreateClaimDTO
            {
                PolicyNumber = "POL123",
                ClaimantName = "test1",
                ClaimantEmail = "test1@example.com",
                ClaimantPhone = "34546",
                ClaimType = "Test",
                ClaimDate = DateTime.Now,
                Files = new List<IFormFile>
                {
                    CreateMockFile("testfile1.txt")
                }
            };

            claimServiceMock.Setup(s => s.CreateClaim(It.IsAny<CreateClaimDTO>()))
                .ReturnsAsync(new Claim { ClaimID = 1 });

            // Act
            var result = await claimController.CreateClaimWithFiles(createDTO);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task CreateClaimWithFiles_NoFiles_ReturnsBadRequest()
        {
            // Arrange
            var createDTO = new CreateClaimDTO
            {
                PolicyNumber = "POL123",
                ClaimantName = "test1",
                ClaimantEmail = "test1@example.com",
                ClaimantPhone = "34546",
                ClaimType = "Test",
                ClaimDate = DateTime.Now,
                Files = new List<IFormFile>() // No files
            };

            // Act
            var result = await claimController.CreateClaimWithFiles(createDTO);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("At least one file must be uploaded.", badRequestResult.Value);
        }

        [Test]
        public async Task GetAllClaims_ReturnsOk()
        {
            // Arrange
            var claims = new List<Claim>
            {
                new Claim { ClaimID = 1, PolicyNumber = "POL123" },
                new Claim { ClaimID = 2, PolicyNumber = "POL124" }
            };

            claimServiceMock.Setup(s => s.GetAll())
                .ReturnsAsync(claims);

            // Act
            var result = await claimController.GetAllClaims();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.That(((IEnumerable<Claim>)okResult.Value).Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task UpdateClaimStatus_ValidRequest_ReturnsOk()
        {
            // Arrange
            var updateDTO = new UpdateClaimStatusDTO
            {
                ClaimID = 1,
                Status = Status.Approved,
            };

            claimServiceMock.Setup(s => s.UpdateClaimStatus(It.IsAny<UpdateClaimStatusDTO>()))
                .ReturnsAsync(new Claim { ClaimID = 1 });

            // Act
            var result = await claimController.UpdateClaimStatus(updateDTO);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task CreateClaimWithFiles_MissingPolicyNumber_ReturnsBadRequest()
        {
            // Arrange
            claimController.ModelState.AddModelError("PolicyNumber", "Policy Number must be at least 6 characters long");
            var createDTO = new CreateClaimDTO
            {
                PolicyNumber = null, // Missing PolicyNumber
                ClaimantName = "test1",
                ClaimantEmail = "test1@example.com",
                ClaimantPhone = "34546",
                ClaimType = "Test",
                ClaimDate = DateTime.Now,
                Files = new List<IFormFile> { CreateMockFile("testfile1.txt") }
            };

            // Act
            var result = await claimController.CreateClaimWithFiles(createDTO);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("At least one file must be uploaded.", badRequestResult.Value);
        }

        [Test]
        public async Task CreateClaimWithFiles_InvalidEmailFormat_ReturnsBadRequest()
        {
            // Arrange
            claimController.ModelState.AddModelError("ClaimantEmail", "ClaimantEmail cannot be blank");
            var createDTO = new CreateClaimDTO
            {
                PolicyNumber = "POL123",
                ClaimantName = "test1",
                ClaimantEmail = null, // Invalid email
                ClaimantPhone = "34546",
                ClaimType = "Test",
                ClaimDate = DateTime.Now,
                Files = new List<IFormFile> { CreateMockFile("testfile1.txt") }
            };

            // Act
            var result = await claimController.CreateClaimWithFiles(createDTO);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("one or more fields validate error", ((ErrorResponseDTO)badRequestResult.Value).ErrorMessage);
        }

        [Test]
        public async Task CreateClaimWithFiles_InvalidClaimType_ReturnsBadRequest()
        {
            // Arrange
            var createDTO = new CreateClaimDTO
            {
                PolicyNumber = "POL123",
                ClaimantName = "test1",
                ClaimantEmail = "test1@example.com",
                ClaimantPhone = "34546",
                ClaimType = "InvalidClaimType",
                ClaimDate = DateTime.Now,
                Files = new List<IFormFile> { CreateMockFile("testfile1.txt") }
            };

            // Act
            var result = await claimController.CreateClaimWithFiles(createDTO);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("one or more fields validate error", ((ErrorResponseDTO)badRequestResult.Value).ErrorMessage);
        }

        [Test]
        public async Task GetAll_ReturnsBadRequest()
        {
            var expectedErrorMessage = "An error occurred while retrieving claims.";
            claimServiceMock.Setup(s => s.GetAll()).ThrowsAsync(new Exception(expectedErrorMessage));

            // Act
            var result = await claimController.GetAllClaims();

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(500, ((ErrorResponseDTO)badRequestResult.Value).ErrorCode);
        }


        private IFormFile CreateMockFile(string fileName)
        {
            var fileContent = "This is a test file content";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(fileContent);
            writer.Flush();
            stream.Position = 0; // Reset the stream position

            var file = new Mock<IFormFile>();
            file.Setup(f => f.FileName).Returns(fileName);
            file.Setup(f => f.Length).Returns(stream.Length);
            file.Setup(f => f.OpenReadStream()).Returns(stream);
            file.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Callback<Stream, CancellationToken>((s, _) => stream.CopyTo(s));

            return file.Object;
        }
    }
}
