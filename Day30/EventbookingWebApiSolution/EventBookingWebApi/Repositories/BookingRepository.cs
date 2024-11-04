using EventBookingWebApi.Context;
using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models;
using EventBookingWebApi.Exceptions;

namespace EventBookingWebApi.Repositories
{
    public class BookingRepository : IRepository<int, Booking>
    {
        private readonly EventContext _context;

        public BookingRepository(EventContext context)
        {
            _context = context;
        }
        public async Task<Booking> Add(Booking entity)
        {
            try
            {
                await _context.Bookings.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception) 
            {
                throw new CouldNotAddException("Booking");
            }
        }

        public async Task<Booking> Delete(int key)
        {
            var deleteBooking = await Get(key);
            if (deleteBooking != null) {
                 _context.Bookings.Remove(deleteBooking);
                await _context.SaveChangesAsync();
                return deleteBooking;
            }
            throw new NotFoundException("Booking");
        }

        public async Task<Booking> Get(int key)
        {
            var booking = _context.Bookings.FirstOrDefault(x => x.BookingId == key);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            var bookings = _context.Bookings.ToList();
            if (bookings.Any()) 
            {
                return bookings;
            }
            throw new CollectionEmptyException("Booking");
        }

        public Task<Booking> Update(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
