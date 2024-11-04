using EFCoreWebAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using EFCoreWebAPI.Repositories;
using EFCoreWebAPI.Models;
using Microsoft.Extensions.Options;
using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Exceptions;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework.Internal;
namespace EFCoreWebAPITest
{
    public class ProductRepositoryTest
    {
        DbContextOptions options;
        ProductRepository repository;
        ShoppingContext shoppingContext;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            shoppingContext = new ShoppingContext(options);
            repository = new ProductRepository(shoppingContext);
        }

        //[Test]

        //[TestCase("Product1", 100, 5, "","Test Product 1", 1)]
        //[TestCase("Product2", 100, 5, "","Test Product 2", 2)]

        //public async Task TestAdd(string name, float price, int qty, string image, string desc, int id)
        //{
        //    //Arrange

        //    Product product = new Product
        //    {
        //        Name = name,
        //        Price = price,
        //        Quantity = qty,
        //        BasicImage = image,
        //        Description = desc
        //    };
        //    //Act


        //    var result = await repository.Add(product);

        //    //Assert
        //    Assert.AreEqual(id, result.Id);
        //}

        [Test]
        [TestCase("Product3", 100, 5, "", "Test Product 3", 3)]
        public async Task GetProductTest(string name, float price, int qty, string image, string desc, int id)
        {
            //Arrange
            Product product = new Product
            {
                Name = name,
                Price = price,
                Quantity = qty,
                BasicImage = image,
                Description = desc
            };

            //Act

            await repository.Add(product);
            var getProduct = await repository.Get(id);

            //Assert
            Assert.AreEqual(getProduct.Id, id);

        }

        [Test]
        public async Task GetAllProductTest()
        {
            //Arrange
            Product product = new Product
            {
                Name = "Product4",
                Price = 100,
                Quantity = 5,
                BasicImage = "Test Product",
                Description = "Test Product"
            };

            //Act

            await repository.Add(product);
            var getProduct = await repository.GetAll();

            //Assert
            Assert.NotNull(getProduct);

        }

        [Test]
        [TestCase("Product1", 100, 5, "", "Test Product 1", 1)]
        public async Task DeleteProduct(string name, float price, int qty, string image, string desc, int id)
        {
            //Arrange
            Product product = new Product
            {
                Name = name,
                Price = price,
                Quantity = qty,
                BasicImage = image,
                Description = desc
            };

            //Act

            await repository.Add(product);
            var deleteProduct = await repository.Delete(id);

            //Assert
            Assert.AreEqual(deleteProduct.Id, id);

        }

        //Exception Testing

        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]

        public async Task TestCouldNotAddException(string name, float price, int quantity, string image, string desc, int id)
        {
            //Arrange
            Product product = new Product
            {
                Name = null,
                Price = price,
                Quantity = quantity,
                BasicImage = image,
                Description = desc
            };

            //Assert
            var exception = Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(product));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Not Add into Product");

        }

        [Test]
        public async Task CollectionEmptyException_GetAll()
        {
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        [TestCase(2)]
        public async Task NotDeleteException_Delete(int id)
        {
            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(id));
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "Not Found in Product. Product Delete Fail");
        }
    }
}
