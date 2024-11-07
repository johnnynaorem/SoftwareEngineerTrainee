using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Repositories;
using NUnit.Framework;

namespace MovieRentWebApiTesting
{
    public class CustomerRepositoryTests
    {
        private MovieRentContext _context;
        private CustomerRepository _customerRepository;

        [SetUp]
        public void Setup()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<MovieRentContext>()
                .UseInMemoryDatabase(databaseName: "MovieRentDb")
                .Options;

            _context = new MovieRentContext(options);
            _customerRepository = new CustomerRepository(_context);

        }

        private void SeedDatabase()
        {
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FullName = "John Doe", Address = "123 Main St", PhoneNumber = "123-456-7890", UserId = 1 },
                new Customer { CustomerId = 2, FullName = "Jane Smith", Address = "456 Oak St", PhoneNumber = "987-654-3210", UserId = 2 }
            };

            _context.Customers.AddRange(customers);
            _context.SaveChanges();
        }

        #region Add Tests

        [Test]
        public async Task Add_ShouldAddNewCustomer_WhenCustomerDoesNotExist()
        {
            // Arrange
            var newCustomer = new Customer
            {
                FullName = "Alice Brown",
                Address = "789 Birch St",
                PhoneNumber = "555-1234",
                UserId = 3
            };

            // Act
            var addedCustomer = await _customerRepository.Add(newCustomer);

            // Assert
            Assert.NotNull(addedCustomer);
            Assert.AreEqual(newCustomer.FullName, addedCustomer.FullName);
            Assert.AreEqual(newCustomer.PhoneNumber, addedCustomer.PhoneNumber);
            Assert.AreEqual(newCustomer.Address, addedCustomer.Address);
            Assert.AreEqual(newCustomer.UserId, addedCustomer.UserId);
        }

        [Test]
        public void Add_ShouldThrowCouldNotAddException_WhenCustomerAlreadyExists()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                FullName = "John Doe",
                Address = "123 Main St",
                PhoneNumber = "123-456-7890",
                UserId = 1
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<CouldNotAddException>(async () =>
                await _customerRepository.Add(existingCustomer)
            );
            Assert.AreEqual("Existing Customer... Add Failed", exception.Message);
        }

        #endregion

        #region Get Tests

        [Test]
        public async Task Get_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Act
            var customer = await _customerRepository.Get(1);

            // Assert
            Assert.NotNull(customer);
            Assert.AreEqual(1, customer.CustomerId);
            Assert.AreEqual("John Doe", customer.FullName);
        }

        [Test]
        public async Task Get_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            // Act
            var customer = await _customerRepository.Get(999);

            // Assert
            Assert.Null(customer);
        }

        #endregion

        #region GetAll Tests

        [Test]
        public async Task GetAll_ShouldReturnAllCustomers_WhenCustomersExist()
        {
            // Act
            var customers = await _customerRepository.GetAll();

            // Assert
            Assert.IsNotEmpty(customers);
            Assert.AreEqual(2, customers.Count());
        }

        [Test]
        public void GetAll_ShouldThrowEmptyCollectionException_WhenNoCustomersExist()
        {

            var exception = Assert.ThrowsAsync<EmptyCollectionException>(async () =>
                await _customerRepository.GetAll()
            );
            Assert.AreEqual("Customers Collection Empty", exception.Message);
        }

        #endregion

        #region Update Tests

        [Test]
        public async Task Update_ShouldUpdateCustomerDetails_WhenCustomerExists()
        {
            // Arrange
            var updatedCustomer = new Customer
            {
                FullName = "Johnathan Doe",
                Address = "123 Main St, Apt 101",
                PhoneNumber = "123-456-7890"
            };

            // Act
            var customer = await _customerRepository.Update(updatedCustomer, 1);

            // Assert
            Assert.NotNull(customer);
            Assert.AreEqual("Johnathan Doe", customer.FullName);
            Assert.AreEqual("123 Main St, Apt 101", customer.Address);
        }

        [Test]
        public void Update_ShouldThrowKeyNotFoundException_WhenCustomerDoesNotExist()
        {
            // Arrange
            var updatedCustomer = new Customer
            {
                FullName = "Nonexistent Customer",
                Address = "Unknown Address",
                PhoneNumber = "000-000-0000"
            };

            // Act & Assert
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _customerRepository.Update(updatedCustomer, 999)
            );
            Assert.AreEqual("Customer with ID 999 not found.", exception.Message);
        }

        #endregion

        #region SaveChangesAsync Tests

        [Test]
        public async Task SaveChangesAsync_ShouldSaveChangesToDatabase()
        {
            // Arrange
            var newCustomer = new Customer
            {
                FullName = "Charlie Brown",
                Address = "101 Pine St",
                PhoneNumber = "555-4321",
                UserId = 4
            };

            // Act
            await _customerRepository.Add(newCustomer);
            await _customerRepository.SaveChangesAsync();

            // Assert
            var savedCustomer = await _customerRepository.Get(3); // Assuming this is the new customer ID
            Assert.NotNull(savedCustomer);
            Assert.AreEqual("Charlie Brown", savedCustomer.FullName);
        }

        #endregion
    }
}
