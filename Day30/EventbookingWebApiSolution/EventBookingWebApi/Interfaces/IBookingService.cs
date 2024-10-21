using EventBookingWebApi.Models;
using EventBookingWebApi.Models.DTOs;

namespace EventBookingWebApi.Interfaces
{
    public interface IBookingService
    {
        Task<int> CreateBooking(BookingDTO booking);
        Task<IEnumerable<Booking>> GetAllBooking();
    }
}
