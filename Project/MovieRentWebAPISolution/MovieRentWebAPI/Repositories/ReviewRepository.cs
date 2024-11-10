using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Repositories
{
    public class ReviewRepository : IRepository<int, ReviewForMovie>
    {
        private readonly MovieRentContext _context;

        public ReviewRepository(MovieRentContext movieRentContext)
        {
            _context = movieRentContext;
        }
        public async Task<ReviewForMovie> Add(ReviewForMovie entity)
        {
            try
            {
                var newReview = await _context.Reviews.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) {
                throw new CouldNotAddException("Review Add Failed");
            }
        }

        public async Task<ReviewForMovie> Delete(int key)
        {
            var review = await Get(key);
            if (review != null)
            {
                 _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return review;
            }
            throw new Exception("Review Delete Failed");
        }

        public async Task<ReviewForMovie> Get(int key)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == key);
            return review;
        }

        public async Task<IEnumerable<ReviewForMovie>> GetAll()
        {
            var reviews = await _context.Reviews.ToListAsync();
            if (reviews.Any())
            {
                return reviews;
            }
            throw new EmptyCollectionException("Reviews Collection Empty");
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ReviewForMovie> Update(ReviewForMovie entity, int key)
        {
            var review = await Get(key);
            if (review == null) {
                throw new InvalidOperationException($"Review not found with Id: {key}");
            }

            if (!string.IsNullOrWhiteSpace(entity.Comment))
            {
                review.Comment = entity.Comment;
            }

            if (entity.Rating.HasValue)
            {
                review.Rating = entity.Rating;
            }

            await _context.SaveChangesAsync();
            return review;
        }
    }
}
