using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Repositories
{
    public class RentalRepository : IRepository<int, Rental>
    {
        private readonly MovieRentContext _context;

        public RentalRepository(MovieRentContext context)
        {
            _context = context;
        }
        public async Task<Rental> Add(Rental entity)
        {
            try
            {
                await _context.Rentals.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new CouldNotAddException("Rental Add Failed");
            }
        }

        public Task<Rental> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Rental> Get(int key)
        {
            var rental =  _context.Rentals.FirstOrDefault(r => r.RentalId == key);
            return rental;
        }

        public async Task<IEnumerable<Rental>> GetAll()
        {
            var rentals = await _context.Rentals.ToListAsync();
            if (rentals.Any())
            {
                return rentals;
            }
            throw new EmptyCollectionException("Rentals Collection Empty");
        }

        public async Task<Rental> Update(Rental entity, int key)
        {

            try
            {
                var rental = await Get(key);

                if (rental == null)
                    throw new KeyNotFoundException($"Rental with ID {key} not found.");

                if (rental.ReturnDate != null) 
                    rental.ReturnDate = entity.ReturnDate;

                if (rental.Status!= RentalStatus.Pending) 
                    rental.Status = entity.Status;

                await _context.SaveChangesAsync();

                return rental;
            }
            catch (KeyNotFoundException ex)
            {
                throw new CouldNotUpdateException("Rental not found..." + ex.Message);
            }
            catch (DbUpdateException ex)
            {
                throw new CouldNotUpdateException("Failed to update the rental due to a database issue." + ex.Message);
            }
            catch (Exception ex)
            {
                throw new CouldNotUpdateException("Rental update failed due to an unexpected error." + ex.Message);
            }
        }
    }
}
