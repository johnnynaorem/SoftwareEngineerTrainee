using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IRepository<int, Wishlist> _wishlistRepo;
        private readonly IRepository<int, Movie> _movieRepo;
        private readonly IRepository<int, Customer> _customerRepo;

        public WishlistService(IRepository<int, Wishlist> wishlistRepo, IRepository<int, Movie> movieRepo, IRepository<int, Customer> customerRepo)
        {
            _wishlistRepo = wishlistRepo;
            _movieRepo = movieRepo;
            _customerRepo = customerRepo;
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

        public async Task<Wishlist> GetWishlistByMovieIdAndCustomerId(int movieId, int customerId)
        {
            var wishlists = await _wishlistRepo.GetAll();
            var wishlist = wishlists.FirstOrDefault(w => w.MovieId == movieId && w.CustomerId == customerId);
            return wishlist;
        }

        public async Task<IEnumerable<WishlistWithMovieAndCustomerDetails>> GetWishlistsByCustomer(int customerId)
        {
            var wishlists = await _wishlistRepo.GetAll();
            var returnWishlist = wishlists.Where(w => w.CustomerId == customerId).ToList();
            var result = new List<WishlistWithMovieAndCustomerDetails>().ToList();

            foreach (var item in returnWishlist)
            {
                var movie = await _movieRepo.Get(item.MovieId);
                var customer = await _customerRepo.Get(item.CustomerId);

                var responseDTO = new WishlistWithMovieAndCustomerDetails
                {
                    WishlistId = item.WishlistId,
                    Movie = new MovieDetailsDTO
                    {
                        MovieId = movie.MovieId,
                        Title = movie.Title,
                        Genre = movie.Genre,
                        RentalPrice = movie.Rental_Price,
                        CoverImage = movie.CoverImage,
                        Rating = movie.Rating,
                        Description = movie.Description,
                        ReleaseDate = movie.ReleaseDate,
                        AvailableCopies = movie.AvailableCopies
                    },
                    Customer = new CustomerResponseDTO
                    {
                        FullName = customer.FullName,
                        PhoneNumber = customer.PhoneNumber,
                        Address = customer.Address,
                        Email = customer.Email,
                    }
                };
                result.Add(responseDTO);
            }
            return result;
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
                throw new InvalidOperationException($"Wishlist is Not Founded with MovieId: {wishlist.MovieId} & CustomerId: {wishlist.CustomerId}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
