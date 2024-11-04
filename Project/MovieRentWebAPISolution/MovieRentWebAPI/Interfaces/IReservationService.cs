using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IReservationService
    {
        Task<int> ReserverMovie(ReserveMovieDTO reserveMovie);
        Task<Reservation> UpdateMovieReservationStatus(ReservedMovieStatusUpdateRequestDTO entity);
    }
}
