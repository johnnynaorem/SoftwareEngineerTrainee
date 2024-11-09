using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IMovieService
    {
        Task<int> CreateMovie(CreateMovieDTO movieDTO);
        Task<IEnumerable<Movie>> GetAll();
        Task<PaginatedResponseDTO<Movie>> GetAll(int pageNumber, int pageSize);
        Task<Movie> GetMovie(int key);
        Task<IEnumerable<Movie>> FilterMoviesAsync(MovieFilterDTO filter);
        Task<int> UpdateMovie(CreateMovieDTO movieDTO, int key);
    }
}
