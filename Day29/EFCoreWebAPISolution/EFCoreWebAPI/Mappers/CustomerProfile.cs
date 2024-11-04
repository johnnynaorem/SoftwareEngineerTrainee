using AutoMapper;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTO;

namespace EFCoreWebAPI.Mappers
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile() { 
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
        }
    }
}
