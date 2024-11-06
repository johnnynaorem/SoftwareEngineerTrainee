using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRepository<int, Rental> _rentalRepo;
        private readonly ILogger<RentalService> _logger;
        private readonly IRepository<int, Movie> _movieRepo;
        private readonly IReservationService _movieRervationService;

        public RentalService(IRepository<int, Rental> rentalRepository, IRepository<int, Movie> movieRepo, ILogger<RentalService> logger, IReservationService reservationService)
        {
            _rentalRepo = rentalRepository;
            _logger = logger;
            _movieRepo = movieRepo;
            _movieRervationService = reservationService;
        }
        public async Task<IEnumerable<Rental>> GetRentals()
        {
            var rentals = await _rentalRepo.GetAll();
            return rentals;
        }

        private async Task<Movie> GetMovie(int id) {
            var movie = await _movieRepo.Get(id);
            return movie;
        }

        private async Task<bool> IsReservedMovie(int movieId)
        {
            var reservations = await _movieRervationService.GetAll();
            var isMovieReserved = (
                                    from movie in reservations
                                    where movie.MovieId == movieId && movie.Status == ReservationStatus.Active
                                    select movie.MovieId
                                   ).ToList();
            if (!isMovieReserved.Any()) return false;

            return true;
        }

        public async Task<string> RentMovie(RentMovieDTO rentMovieDTO)
        {
            try
            {
                _logger.LogInformation("Processing rental for movie ID: " + rentMovieDTO.MovieId);

                var movie = await GetMovie(rentMovieDTO.MovieId);
                if (movie == null)
                {
                    throw new InvalidOperationException("Movie not Found");
                }

                bool isMovieReserved = await IsReservedMovie(movie.MovieId);
                if (!isMovieReserved)
                {
                    _logger.LogWarning("Movie with ID " + rentMovieDTO.MovieId + " is not reserved.");
                    throw new MovieNotReservedException("You are not able to rent this movie because it is not reserved or the reservation status is not 'Active'");
                }

                var rental = new Rental
                {
                    CustomerId = rentMovieDTO.CustomerId,
                    MovieId = rentMovieDTO.MovieId,
                    RentalDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    RentalFee = movie.Rental_Price,
                };

                var addRental = await _rentalRepo.Add(rental);
                _logger.LogInformation("Successfully rented movie with ID: " + addRental.RentalId);

                return "Successfully rented movie with ID: " + addRental.RentalId.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while renting movie.");
                throw;
            }
        }


        public async Task<Rental> Update(RentalUpdateRequestDTO rentalUpdateRequestDTO)
        {
            var rental = new Rental
            {
                Status = rentalUpdateRequestDTO.Status,
                ReturnDate = rentalUpdateRequestDTO?.ReturnDate,
            };

            var updatedRental = await _rentalRepo.Update(rental, rentalUpdateRequestDTO.RentalId);

            return updatedRental;
        }
    }
}
