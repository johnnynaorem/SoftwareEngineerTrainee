using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer();
        Task<int> UpdateCustomer();
        Task<IEnumerable<Customer>> GetAllCustomer();
        Task<Customer> GetCustomerById(int id);

    }
}
