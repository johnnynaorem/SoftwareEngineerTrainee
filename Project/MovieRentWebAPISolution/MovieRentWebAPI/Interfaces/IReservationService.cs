using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IReservationService
    {
        Task<int> ReserverMovie(ReserveMovieDTO reserveMovie);
        Task<ReservedStatusUpdateResponseDTO> UpdateMovieReservationStatus(ReservedMovieStatusUpdateRequestDTO entity);

        Task<IEnumerable<ReservationWithMovieAndCustomerDto>> GetAllByCustomer(int customerId);
        Task<ReservationReturnDTO> GetReservationById(int movieId);
        Task<IEnumerable<Reservation>> GetAll();
    }
}
