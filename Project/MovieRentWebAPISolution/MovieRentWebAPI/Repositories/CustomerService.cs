using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, Customer> _customerRepo;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IRepository<int, Customer> customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepo = customerRepository;
            _logger = logger;
        }
        public Task<int> CreateCustomer()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllCustomer()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateCustomer()
        {
            throw new NotImplementedException();
        }
    }
}
