using MovieRentWebAPI.Context;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Exceptions;

namespace MovieRentWebAPI.Repositories
{
    public class WishlistRepository : IRepository<int, Wishlist>
    {
        private readonly MovieRentContext _context;

        public WishlistRepository(MovieRentContext context)
        {
            _context = context;
        }
        public async Task<Wishlist> Add(Wishlist entity)
        {
            var wishlist = _context.Wishlists.FirstOrDefault(w => w.MovieId == entity.MovieId && w.CustomerId == entity.CustomerId);
            if (wishlist == null) {
                await _context.Wishlists.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            throw new InvalidOperationException("Wishlist AlreadyExist");
        }

        public async Task<Wishlist> Delete(int key)
        {
            var wishlist = await Get(key);
            if (wishlist != null) {
                var deleteWishlist = _context.Wishlists.Remove(wishlist);
                await _context.SaveChangesAsync();
                return wishlist;
            }
            throw new InvalidOperationException("Wishlist Not Found");
        }

        public async Task<Wishlist> Get(int key)
        {
            var wishlist =  _context.Wishlists.FirstOrDefault(w => w.WishlistId == key);
            return wishlist;
        }

        public async Task<IEnumerable<Wishlist>> GetAll()
        {
            var wishlists =  _context.Wishlists.ToList();
            if (wishlists.Any()) {
                return wishlists;
            }
            throw new EmptyCollectionException("Wishlists Collection Empty");
        }

        public async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Wishlist> Update(Wishlist entity, int key)
        {
            throw new NotImplementedException();
        }
    }
}
