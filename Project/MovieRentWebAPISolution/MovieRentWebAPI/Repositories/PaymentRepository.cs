using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Repositories
{
    public class PaymentRepository : IRepository<int, Payment>
    {
        private readonly MovieRentContext _context;

        public PaymentRepository(MovieRentContext movieRentContext)
        {
            _context = movieRentContext;
        }
        public async Task<Payment> Add(Payment entity)
        {
            try
            {
                await _context.Payments.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw new CouldNotAddException($"Payment Failed: {ex.Message}");
            }
        }

        public Task<Payment> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> Get(int key)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.paymentId == key);
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            var payments =  _context.Payments.ToList();
            if (payments.Any())
            {
                return payments;
            }
            throw new EmptyCollectionException("Payment Collection Empty");
        }

        public Task<Payment> Update(Payment entity, int key)
        {
            throw new NotImplementedException();
        }
    }
}
