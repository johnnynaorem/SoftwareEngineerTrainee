using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventBookingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eService)
        {
            _eventService = eService;
        }

        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = await _eventService.GetAllEvent();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvent()
        {
            try
            {
                var events = await _eventService.GetAllEvent();
                //This is LINQ Basic query to fetch Only Title in asc order
                //This is LINQ query method
                var orderEvents = (from moreEvent in events orderby moreEvent.Title select moreEvent.Title).ToList();
                return Ok(orderEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent(EventDTO entity)
        {
            try
            {
                var newEvent= await _eventService.CreateEvent(entity);
                return Ok(newEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
