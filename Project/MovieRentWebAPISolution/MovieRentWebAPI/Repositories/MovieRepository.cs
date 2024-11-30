using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Repositories
{
    public class MovieRepository : IRepository<int, Movie>
    {
        private readonly MovieRentContext _context;

        public MovieRepository(MovieRentContext context)
        {
            _context = context;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Movie> Add(Movie entity)
        {
            try
            {
                await _context.Movies.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw new CouldNotAddException($"Movie Add Failed: {ex.Message}");
            }
        }

        public async Task<Movie> Delete(int key)
        {
            var deleteMovie = await Get(key);
            if (deleteMovie != null)
            {
                _context.Movies.Remove(deleteMovie);
                await _context.SaveChangesAsync();
                return deleteMovie;
            }
            throw new Exception("Delete Movie Failed");
        }

        public async Task<Movie> Get(int key)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.MovieId == key);
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            var movies = _context.Movies.ToList();
            if (movies.Any())
            {
                return movies;
            }
            throw new EmptyCollectionException("Movies Collection Empty");
        }

        public async Task<Movie> Update(Movie entity, int key)
        {
            var updateMovie = await Get(key);
            if (updateMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {key} not found.");
            }

            if (!string.IsNullOrWhiteSpace(entity.Title))
            {
                updateMovie.Title = entity.Title;
            }

            if (!string.IsNullOrWhiteSpace(entity.Genre))
            {
                updateMovie.Genre = entity.Genre;
            }

            if (!string.IsNullOrWhiteSpace(entity.Description))
            {
                updateMovie.Description = entity.Description;
            }

            if (entity.Rating.HasValue)
            {
                updateMovie.Rating = entity.Rating;
            }

            if (entity.AvailableCopies >= 0) 
            {
                updateMovie.AvailableCopies = entity.AvailableCopies;
            }

            if (entity.Rental_Price != updateMovie.Rental_Price)
            {
                updateMovie.Rental_Price = entity.Rental_Price;
            }
            if (!string.IsNullOrWhiteSpace(entity.CoverImage))
            {
                updateMovie.CoverImage = entity.CoverImage;
            }

            await _context.SaveChangesAsync();
            return updateMovie;
            
        }
    }
}
