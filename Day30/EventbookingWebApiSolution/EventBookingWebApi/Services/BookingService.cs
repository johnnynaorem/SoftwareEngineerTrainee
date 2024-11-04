using AutoMapper;
using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models;
using EventBookingWebApi.Models.DTOs;

namespace EventBookingWebApi.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<int, Booking> _bookingRepo;

        public BookingService(IRepository<int, Booking> bookingRepository, IMapper mapper)
        {
            _mapper = mapper;
            _bookingRepo = bookingRepository;
        }
        public async Task<int> CreateBooking(BookingDTO booking)
        {
            Booking newBooking = _mapper.Map<Booking>(booking);
            var addBooking = await _bookingRepo.Add(newBooking);
            return addBooking.BookingId;
        }

        public async Task<IEnumerable<Booking>> GetAllBooking()
        {
            var bookings = await _bookingRepo.GetAll();
            return bookings;
        }
    }
}
