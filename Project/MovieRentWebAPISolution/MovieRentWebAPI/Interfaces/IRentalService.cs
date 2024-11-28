using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IRentalService
    {
        Task<int> RentMovie(RentMovieDTO rentMovieDTO);
        Task<IEnumerable<RentalWithMovieAndCustomerDetailsDTO>> GetRentals();
        Task<IEnumerable<RentalWithMovieAndCustomerDetailsDTO>> GetRentalByCustomer(int customerId);
        Task<Rental> Update(RentalUpdateRequestDTO rentalUpdateRequestDTO);
    }
}
