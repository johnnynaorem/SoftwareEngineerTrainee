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
    public class ClaimTypeControllerTesting
    {
        DbContextOptions options;
        PolicyContext context;
        ClaimTypeRepository repository;
        ClaimTypeController claimTypeController;
        ClaimTypeService claimTypeService;
        ClaimantRepository claimantRepository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<PolicyContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new PolicyContext((DbContextOptions<PolicyContext>)options);
            repository = new ClaimTypeRepository(context);
            claimantRepository = new ClaimantRepository(context);
            claimTypeService = new ClaimTypeService(repository);
            claimTypeController = new ClaimTypeController(claimTypeService);
        }

        [Test]
        public async Task claimTypeController_StatusCode200_Test()
        {
            var claimType = new ClaimTypeDTO
            {
                PolicyNumber = "POL123",
                TypeDescription = "test",
                TypeName = "Test"
                
            };

            var expectedClaimType = new ClaimType
            {
                PolicyNumber = "POL123",
                TypeDescription = "test",
                TypeName = "Test"
            };

            // Act
            var result = await claimTypeController.CreatePolicy(claimType);
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        //Exceptions

        [Test]
        public async Task Validate_Fail_Exception_ClaimType()
        {
            claimTypeController.ModelState.AddModelError("PolicyNumber", "Policy Number must be at least 6 characters long");
            var claimType = new ClaimTypeDTO
            {
                PolicyNumber = "POL",
                TypeDescription = "test",
                TypeName = "Test"

            };

          

            var result = await claimTypeController.CreatePolicy(claimType);
            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

    }
}
