using AutoMapper;
using EventBookingWebApi.Models.DTOs;
using EventBookingWebApi.Models;

namespace EventBookingWebApi.Mappers
{
    public class BookingProfile:Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingDTO, Booking>();
        }
    }
}
