using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepository<int, Wishlist> _wishlistRepo;

        public WishlistService(IRepository<int, Wishlist> wishlistRepo)
        {
            _wishlistRepo = wishlistRepo;
        }
        public async Task<bool> Add(AddWishlistDTO wishlist)
        {
            try
            {
                var addWishlist = new Wishlist
                {
                    CustomerId = wishlist.CustomerId,
                    MovieId = wishlist.MovieId,
                };
                var addedWishlist = await _wishlistRepo.Add(addWishlist);
                return true;
            }
            catch (Exception ex) { 
                return false;
            }
           
        }

        public async Task<bool> RemoveWishlist(AddWishlistDTO wishlist)
        {
            try
            {
                var wishlists = await _wishlistRepo.GetAll();
                var isExist =  wishlists.FirstOrDefault(w => w.MovieId == wishlist.MovieId && w.CustomerId == wishlist.CustomerId);
                if (isExist != null)
                {
                    var removeWishlist = await _wishlistRepo.Delete(isExist.WishlistId);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
