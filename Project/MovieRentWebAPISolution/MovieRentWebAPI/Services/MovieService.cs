﻿using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Services
{
    public class MovieService : IMovieServive
    {
        private readonly IRepository<int, Movie> _movieRepo;
        private readonly ILogger<MovieService> _logger;

        public MovieService(IRepository<int, Movie> movieRepository, ILogger<MovieService> logger)
        {
            _logger = logger;
            _movieRepo = movieRepository;
        }
        public async Task<int> CreateMovie(CreateMovieDTO movieDTO)
        {
            var movie = new Movie()
            {
                Title = movieDTO.Title,
                Genre = movieDTO.Genre,
                Description = movieDTO.Description,
                Rental_Price = movieDTO.Rental_Price,
                CoverImage = movieDTO.CoverImage,
                AvailableCopies = movieDTO.AvailableCopies,
                Rating = movieDTO.Rating,
                ReleaseDate = movieDTO.ReleaseDate,
            };

            var addMovie =  await _movieRepo.Add(movie);
            return addMovie.MovieId;

        }

        public async Task<IEnumerable<Movie>> FilterMoviesAsync(MovieFilterDTO filter)
        {
            var allMovies = await _movieRepo.GetAll();

            var filteredMovies = allMovies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                filteredMovies = filteredMovies.Where(m => m.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter.Genre))
            {
                filteredMovies = filteredMovies.Where(m => m.Genre.Equals(filter.Genre, StringComparison.OrdinalIgnoreCase));
            }

            if (filter.Rating.HasValue)
            {
                filteredMovies = filteredMovies.Where(m => m.Rating == filter.Rating);
            }

            if (filter.ReleaseDate.HasValue)
            {
                filteredMovies = filteredMovies.Where(m => m.ReleaseDate.Date == filter.ReleaseDate.Value.Date);
            }

            if (filter.MinimumRating.HasValue)
            {
                filteredMovies = filteredMovies.Where(m => m.Rating >= filter.MinimumRating.Value);
            }

            return filteredMovies.ToList();
        }

        public Task<IEnumerable<Movie>> GetAll()
        {
            var movies = _movieRepo.GetAll();
            return movies;
        }

        public async Task<Movie> GetMovie(int key)
        {
            var movie = await _movieRepo.Get(key);
            return movie;
        }

        public async Task<int> UpdateMovie(CreateMovieDTO movieDTO, int key)
        {
            var oldMovie = await _movieRepo.Get(key);
            if(oldMovie != null)
            {
                var movie = new Movie()
                {
                    Title = movieDTO.Title,
                    Genre = movieDTO.Genre,
                    Description = movieDTO.Description,
                    Rental_Price = movieDTO.Rental_Price,
                    CoverImage = movieDTO.CoverImage,
                    AvailableCopies = movieDTO.AvailableCopies,
                    Rating = movieDTO.Rating,
                };
                var updateMovie = await _movieRepo.Update(movie, oldMovie.MovieId);
                return updateMovie.MovieId;
            }
            throw new Exception("Update Failed");
        }
    }
}