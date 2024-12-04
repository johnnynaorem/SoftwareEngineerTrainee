using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IWishlistService
    {
        Task<bool> Add(AddWishlistDTO wishlist);
        Task<bool> RemoveWishlist(AddWishlistDTO wishlist);
        Task<Wishlist> GetWishlistByMovieIdAndCustomerId(int movieId, int customerId);
        Task<IEnumerable<Wishlist>> GetWishlistsByCustomer(int customerId);
    }
}
