using EventBookingWebApi.Models.DTOs;
using EventBookingWebApi.Models;
using AutoMapper;

namespace EventBookingWebApi.Mappers
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();
        }
    }
}
