using EFCoreWebAPI.Models.DTO;

namespace EFCoreWebAPI.Interfaces
{
    public interface ICustomerBasicService
    {
        Task<int> CreateCustomer(CustomerDTO customer);
    }
}
