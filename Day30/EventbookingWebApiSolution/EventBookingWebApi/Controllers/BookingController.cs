using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IEmployeeService _empService;
        private readonly IEventService _eventService;

        public BookingController(IBookingService bookingService, IEventService eventService, IEmployeeService employeeService)
        {
            _bookingService = bookingService;
            _empService = employeeService;
            _eventService = eventService;
        }

        [HttpPost("BookEvent")]
        public async Task <IActionResult> BookEvent(BookingDTO entity)
        {
            try
            {
                var newBookingId = await _bookingService.CreateBooking(entity);
                return Ok(newBookingId);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("ListOfBooking")]
        public async Task<IActionResult> GetAllBooking()
        {
            try
            {
                var bookings = await _bookingService.GetAllBooking();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getEventWithEmployee")]
        public async Task<IActionResult> GetEventWithEmployee()
        {
            try
            {
                var bookings = await _bookingService.GetAllBooking();
                var events = await _eventService.GetAllEvent();
                var employees = await _empService.GetAll();
                var eventWithEmployee = (
                                           from e in employees
                                           join ev in events
                                           on e.EmployeeId equals ev.EmployeeId
                                           select e).ToList();
                return Ok(eventWithEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("getEventWithEmployeeUsingDTO")]
        public async Task<IActionResult> GetEventWithEmployeeUsingDTO()
        {
            try
            {
                var bookings = await _bookingService.GetAllBooking();
                var events = await _eventService.GetAllEvent();
                var employees = await _empService.GetAll();
                var eventWithEmployee = (
                                           from e in employees
                                           join ev in events
                                           on e.EmployeeId equals ev.EmployeeId
                                           join b in bookings
                                           on e.EmployeeId equals b.EmployeeId
                                           select new BookingDTO{ EventId = ev.EventId, EmployeeId = e.EmployeeId, BookingTime = b.BookingTime.ToLocalTime() }).Distinct().ToList();
                return Ok(eventWithEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
