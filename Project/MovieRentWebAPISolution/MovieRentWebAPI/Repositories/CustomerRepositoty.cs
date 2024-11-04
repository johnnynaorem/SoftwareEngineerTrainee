using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Repositories
{
    public class CustomerRepositoty : IRepository<int, Customer>
    {
        private readonly MovieRentContext _context;

        public CustomerRepositoty(MovieRentContext context)
        {
            _context = context;
        }
        public async Task<Customer> Add(Customer entity)
        {
            var customers = await GetAll();
            var customer = customers.FirstOrDefault(c => c.FullName == entity.FullName && c.UserId == entity.UserId);
            if(customer == null)
            {
                customer = new Customer
                {
                    FullName = entity.FullName,
                    UserId = entity.UserId,
                    Address = entity.Address,
                    PhoneNumber = entity.PhoneNumber,
                };
                 _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            throw new CouldNotAddException("Existing Customer... Add Failed");
        }

        public Task<Customer> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> Get(int key)
        {
            var cutomer =  _context.Customers.FirstOrDefault(c => c.CustomerId == key);
            return cutomer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = _context.Customers.ToList();
            if (customers.Any()) { 
                return customers;
            }
            throw new EmptyCollectionException("Customers Collection Empry");
        }

        public Task<Customer> Update(Customer entity, int key)
        {
            throw new NotImplementedException();
        }
    }
}
