using EventBookingWebApi.Models;
using EventBookingWebApi.Models.DTOs;

namespace EventBookingWebApi.Interfaces
{
    public interface IEventService
    {
        Task<int> CreateEvent(EventDTO entity);
        Task <IEnumerable<Event>> GetAllEvent();
    }
}
