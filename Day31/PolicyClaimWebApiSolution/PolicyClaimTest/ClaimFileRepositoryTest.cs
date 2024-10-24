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
    public class ClaimFileRepositoryTest
    {
        DbContextOptions options;
        PolicyContext context;
        ClaimFileRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<PolicyContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;
            context = new PolicyContext((DbContextOptions<PolicyContext>)options);
            repository = new ClaimFileRepository(context);
        }

        [Test]
        public async Task AddClaimFile_Test()
        {
            //Arrangement
            var claimFile = new ClaimFile
            {
                ClaimID = 1,
                FilePath = "test.jpg"
            };

            var addedClaimFile = await repository.Add(claimFile);
            Assert.NotNull(addedClaimFile);

            Assert.AreEqual(1, addedClaimFile.ClaimFileId);
        }

        [Test]
        public async Task GetClaimFile_Test()
        {
            //Arrangement
            var claimFile = new ClaimFile
            {
                ClaimID = 1,
                FilePath = "test.jpg"
            };

            //Act
            await repository.Add(claimFile);
            var result = await repository.GetAll();

            //Assert
            Assert.IsNotNull(result);
        }

        //Exceptions Testing

        [Test]
        public async Task ClaimFileRepositoryAdd_CouldNotAddException()
        {
            var claimFile = new ClaimFile
            {
                ClaimID = 1,
                FilePath = null
            };

            var exception = Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(claimFile));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Fail to add ClaimFile");
        }


        [Test]
        public async Task ClaimFileRepositoryGetAll_EmptyCollectionException()
        {
            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () => await repository.GetAll());
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "claimFiles collection is empty");
        }
    }
}
