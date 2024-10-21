using EventBookingWebApi.Context;
using EventBookingWebApi.Exceptions;
using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EventBookingWebApi.Repositories
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        private readonly EventContext _context;

        public EmployeeRepository(EventContext eventContext)
        {
            _context = eventContext;
        }
        public async Task<Employee> Add(Employee entity)
        {
            try
            {
                await _context.Employees.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Employee");
            }
        }

        public async Task<Employee> Delete(int key)
        {
            var employee = await Get(key);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            throw new NotFoundException("Employee. Employee Delete Fail");
        }

        public async Task<Employee> Get(int key)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(p => p.EmployeeId == key);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var employees = await _context.Employees.ToListAsync();
            if (employees.Any())
            {
                return employees;
            }
            throw new CollectionEmptyException("Employee");
        }

        public Task<Employee> Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
