﻿using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface ICustomerService
    {
        Task<int> CreateCustomer(CreateCustomerDTO customer);
        Task<int> UpdateCustomer(CreateCustomerDTO customerDTO, int key);
        Task<IEnumerable<Customer>> GetAllCustomer();
        Task<Customer> GetCustomerById(int id);

    }
}