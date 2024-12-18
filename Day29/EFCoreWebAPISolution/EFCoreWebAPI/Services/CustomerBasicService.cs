﻿using AutoMapper;
using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Models.DTO;
using EFCoreWebAPI.Repositories;

namespace EFCoreWebAPI.Services
{
    public class CustomerBasicService : ICustomerBasicService
    {
        private readonly IRepository<int, Customer> _customerRepo;
        private readonly IMapper _mapper;

        public CustomerBasicService(IRepository<int, Customer> customerRepository, IMapper mapper) {
            _customerRepo = customerRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateCustomer(CustomerDTO customer)
        {
            //Customer newCustomer = MapCustomerDTOToCustomer(customer);
            Customer newCustomer = _mapper.Map<Customer>(customer);
            newCustomer.Age = CalculateAgeFromDateTime(customer.DateOfBirth);
            var addedCustomer = await _customerRepo.Add(newCustomer);
            return addedCustomer.Id;
        }

        //private Customer MapCustomerDTOToCustomer(CustomerDTO customer) {
        //    return new Customer
        //    {
        //        Name = customer.Name,
        //        Email = customer.Email,
        //        Phone = customer.Phone,
        //        DateOfBirth = customer.DateOfBirth
        //    };
        //}

        private int CalculateAgeFromDateTime(DateTime dateOfBirth) {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
