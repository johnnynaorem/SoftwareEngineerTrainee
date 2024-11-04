using AutoMapper;
using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models;
using EventBookingWebApi.Models.DTOs;

namespace EventBookingWebApi.Services
{
    public class EventService : IEventService
    {
        private readonly IRepository<int, Event> _eventRepo;
        private readonly IMapper _mapper;

        public EventService(IRepository<int, Event> eventRepository, IMapper mapper)
        {
            _eventRepo = eventRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateEvent(EventDTO entity)
        {
            Event newEvent = _mapper.Map<Event>(entity);
            var addEvent = await _eventRepo.Add(newEvent);
            return addEvent.EventId;
        }

        public async Task<IEnumerable<Event>> GetAllEvent()
        {

            var events = await _eventRepo.GetAll();
            return events;
        }
    }
}
