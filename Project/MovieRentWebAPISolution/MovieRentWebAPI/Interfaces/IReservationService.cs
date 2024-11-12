using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IReservationService
    {
        Task<int> ReserverMovie(ReserveMovieDTO reserveMovie);
        Task<ReservedStatusUpdateResponseDTO> UpdateMovieReservationStatus(ReservedMovieStatusUpdateRequestDTO entity);

        Task<IEnumerable<Movie>> GetAllByCustomer(int customerId);
        Task<IEnumerable<Movie>> GetAllByMovie(int movieId);
        Task<IEnumerable<Reservation>> GetAll();
    }
}
