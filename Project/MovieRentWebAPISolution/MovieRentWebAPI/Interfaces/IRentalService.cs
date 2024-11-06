using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IRentalService
    {
        Task<string> RentMovie(RentMovieDTO rentMovieDTO);
        Task<IEnumerable<Rental>> GetRentals();
        Task<Rental> Update(RentalUpdateRequestDTO rentalUpdateRequestDTO);
    }
}
