using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IMovieServive
    {
        Task<int> CreateMovie(CreateMovieDTO movieDTO);
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetMovie(int key);
        Task<IEnumerable<Movie>> FilterMoviesAsync(MovieFilterDTO filter);
        Task<int> UpdateMovie(CreateMovieDTO movieDTO, int key);
    }
}
