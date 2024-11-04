using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Controllers;
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
    public class ClaimControllerTest
    {
        DbContextOptions options;
        PolicyContext context;
        ClaimRepository repository;
        ClaimFileRepository fileRepository;
        ClaimController claimController;
        ClaimService claimService;
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
            claimService = new ClaimService(repository, fileRepository, claimantRepository);
            claimController = new ClaimController(claimService);
        }

        [Test]
        public async Task claimController_StatusCode200_Test()
        {
            var createDTO = new CreateClaimDTO
            {
                PolicyNumber = "POL123",
                ClaimantName = "test1",
                ClaimantEmail = "test1",
                ClaimantPhone = "34546",
                ClaimType = "Test",
                ClaimDate = DateTime.Now,
            };

            var expectedClaim = new Claim
            {
                PolicyNumber = "POL123",
                ClaimantId = 1,
                ClaimDate = DateTime.Now,
            };
            var controller = new ClaimController(claimService);

            // Act
            var result = await controller.CreateClaimWithFiles(createDTO);
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}
