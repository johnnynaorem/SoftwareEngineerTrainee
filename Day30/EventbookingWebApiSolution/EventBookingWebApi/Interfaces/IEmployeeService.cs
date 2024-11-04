using EventBookingWebApi.Models;
using EventBookingWebApi.Models.DTOs;

namespace EventBookingWebApi.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CreateEmployee(EmployeeDTO employee);
        Task<IEnumerable<Employee>> GetAll();
    }
}
