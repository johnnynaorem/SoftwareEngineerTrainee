using AutoMapper;
using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models;
using EventBookingWebApi.Models.DTOs;

namespace EventBookingWebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<int, Employee> employeeRepository, IMapper mapper) 
        {
            _employeeRepo=employeeRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateEmployee(EmployeeDTO employee)
        {
            Employee newEmployee = _mapper.Map<Employee>(employee);
            newEmployee.Age = CalculateAgeFromDateTime(employee.DateOfBirth);
            var addEmployee = await _employeeRepo.Add(newEmployee);
            return addEmployee.EmployeeId;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var employees = await _employeeRepo.GetAll();
            return employees;
        }

        private int CalculateAgeFromDateTime(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
