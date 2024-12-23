﻿using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Repositories
{
    public class CustomerRepository : IRepository<int, Customer>
    {
        private readonly MovieRentContext _context;

        public CustomerRepository(MovieRentContext context)
        {
            _context = context;
        }
        public async Task<Customer> Add(Customer entity)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.FullName == entity.FullName && c.UserId == entity.UserId);
            if(customer == null)
            {
                 _context.Customers.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            throw new CouldNotAddException("Existing Customer... Add Failed");
        }

        public Task<Customer> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> Get(int key)
        {
            var cutomer =  _context.Customers.FirstOrDefault(c => c.CustomerId == key);
            return cutomer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = _context.Customers.ToList();
            if (customers.Any()) { 
                return customers;
            }
            throw new EmptyCollectionException("Customers Collection Empty");
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> Update(Customer entity, int key)
        {
            var oldCustomer = await Get(key);
            if (oldCustomer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {key} not found.");
            }

            if (!string.IsNullOrWhiteSpace(entity.FullName))
            {
                oldCustomer.FullName = entity.FullName;
            }
            
            if (!string.IsNullOrWhiteSpace(entity.PhoneNumber))
            {
                oldCustomer.PhoneNumber = entity.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(entity.Address))
            {
                oldCustomer.Address = entity.Address;
            }

            await _context.SaveChangesAsync();
            return oldCustomer;
        }
    }
}
