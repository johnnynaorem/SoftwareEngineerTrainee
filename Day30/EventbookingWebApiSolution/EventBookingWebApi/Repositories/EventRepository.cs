using EventBookingWebApi.Context;
using EventBookingWebApi.Exceptions;
using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingWebApi.Repositories
{
    public class EventRepository : IRepository<int, Event>
    {
        private readonly EventContext _context;

        public EventRepository(EventContext eventContext)
        {
            _context = eventContext;
        }
        public async Task<Event> Add(Event entity)
        {
            try
            {
                await _context.Events.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ) {
                throw new CouldNotAddException("Event");
            }
        }

        public async Task<Event> Delete(int key)
        {
            var getEvent = await Get(key);
            if (getEvent != null)
            {
                _context.Events.Remove(getEvent);
                await _context.SaveChangesAsync();
                return getEvent;
            }
            throw new NotFoundException("Event. Event Delete Fail");
        }

        public async Task<Event> Get(int key)
        {
            var getEvent = _context.Events.FirstOrDefault(e => e.EventId == key);
            return getEvent;

        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            var events = await _context.Events.ToListAsync();
            if (events.Any())
            {
                return events;
            }
            throw new CollectionEmptyException("Events");
        }

        public Task<Event> Update(Event entity)
        {
            throw new NotImplementedException();
        }
    }
}
