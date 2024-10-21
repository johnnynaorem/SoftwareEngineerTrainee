using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models;
using EventBookingWebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) {
            _employeeService = employeeService;
        }

        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAll();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetEmployeeName")]
        public async Task<IActionResult> GetEmployeeNames()
        {
            try
            {
                var employees = await _employeeService.GetAll();
                //This is LINQ Basic query to fetch Only Name of Employees of age >= 22 in desc order
                //This is LINQ query method 
                var employeeNames = (
                    from empname in employees
                    where empname.Age >= 22
                    orderby empname.EmployeeId descending
                    select empname.Name
                    )
                    .ToList();
                return Ok(employeeNames);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }   
        }

        [HttpGet("GetEmployeeNameAndEmail")]
        public async Task<IActionResult> GetEmployeeNameAndEmail()
        {
            try
            {
                var employees = await _employeeService.GetAll();
                //This is LINQ Basic query to fetch Name and Email of Employees in asc order
                //This is LINQ query method 
                var employeeEmails = (
                    from emp in employees
                    select new {emp.Name, emp.Email}
                    )
                    .ToList();
                return Ok(employeeEmails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("createEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {
            try
            {
                var newEmployeeId = await _employeeService.CreateEmployee(employee);
                return Ok(newEmployeeId);
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
