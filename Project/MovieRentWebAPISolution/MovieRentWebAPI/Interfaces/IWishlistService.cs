using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IWishlistService
    {
        Task<bool> Add(AddWishlistDTO wishlist);
        Task<bool> RemoveWishlist(AddWishlistDTO wishlist);
    }
}
