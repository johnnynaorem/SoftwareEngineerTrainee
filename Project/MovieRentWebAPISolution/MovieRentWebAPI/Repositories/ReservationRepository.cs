using MovieRentWebAPI.Context;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Repositories
{
    public class ReservationRepository : IRepository<int, Reservation>
    {
        private readonly MovieRentContext _context;

        public ReservationRepository(MovieRentContext movieRentContext)
        {
            _context = movieRentContext;
        }
        public async Task<Reservation> Add(Reservation entity)
        {
            var existingReservation =  _context.Reservations.FirstOrDefault(r => r.CustomerId == entity.CustomerId && r.MovieId == entity.MovieId && r.Status == ReservationStatus.Pending || r.Status==ReservationStatus.Active);
            if(existingReservation != null)
            {
                throw new InvalidOperationException("You cannot reserve the same movie more than once at the same time.");
            }
            await _context.Reservations.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public Task<Reservation> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation> Get(int key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Reservation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> Update(Reservation entity, int key)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.ReservationId == key);
            if (reservation != null)
            {
                reservation.Status = entity.Status;
                await _context.SaveChangesAsync();
                return reservation;
            }
            throw new Exception($"There is no Movie Reservation with this ID: {key}");
        }
    }
}
