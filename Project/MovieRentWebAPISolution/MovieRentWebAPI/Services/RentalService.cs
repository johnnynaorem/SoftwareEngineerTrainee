using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.EmailModels;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using System.Globalization;

namespace MovieRentWebAPI.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRepository<int, Rental> _rentalRepo;
        private readonly ILogger<RentalService> _logger;
        private readonly IRepository<int, Movie> _movieRepo;
        private readonly IReservationService _movieRervationService;
        private readonly IEmailSender _emailSender;
        private readonly IRepository<int, Customer> _customerRepo;

        public RentalService(IRepository<int, Rental> rentalRepository, IRepository<int, Movie> movieRepo, ILogger<RentalService> logger, IReservationService reservationService, IEmailSender emailSender, IRepository<int, Customer> customerRepo)
        {
            _rentalRepo = rentalRepository;
            _logger = logger;
            _movieRepo = movieRepo;
            _movieRervationService = reservationService;
            _emailSender = emailSender;
            _customerRepo = customerRepo;

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

        private void SendMail(string title, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        "johnnynaorem7@gmail.com" },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }

        private async Task CheckAndUpdateOverdueRentals()
        {
            try
            {
                var overdueRentals = await _rentalRepo.GetAll();
                var overdueRentalsToUpdate = overdueRentals
                    .Where(r => r.Status != RentalStatus.Returned && r.Status != RentalStatus.Overdue && r.DueDate < DateTime.Now)
                    .ToList();

                foreach (var rental in overdueRentalsToUpdate)
                {
                    rental.Status = RentalStatus.Overdue;
                    _logger.LogInformation($"Rental {rental.RentalId} marked as overdue.");
                }

                if (overdueRentalsToUpdate.Any())
                {
                    await _rentalRepo.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking and updating overdue rentals.");
            }
        }
        public async Task<string> RentMovie(RentMovieDTO rentMovieDTO)
        {
            try
            {
                await CheckAndUpdateOverdueRentals();

                _logger.LogInformation("Processing rental for movie ID: " + rentMovieDTO.MovieId);

                var movie = await GetMovie(rentMovieDTO.MovieId);
                if (movie == null)
                {
                    throw new InvalidOperationException("Movie not Found");
                }

                var customer = await _customerRepo.Get(rentMovieDTO.CustomerId);
                if(customer == null)
                {
                    throw new InvalidOperationException("Customer not Found");
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
                var rentalFeeFormatted = rental.RentalFee.ToString("C", new CultureInfo("en-IN"));
                string emailBody = $"Dear {customer.FullName},\n\n" +
                                    "Your movie rental has been successfully created!\n\n" +
                                    $"We are happy to inform you that your rental for the movie **{movie.Title}** is now **Pending**\n\n" +
                                    "For Complete Process Please Complete Payment\n\n" +
                                    "You can pay by click the link below:\n\n" +
                                    $"[Payment Link](https://localhost:7203/swagger/index.html)\n\n" +
                                    "Here are your rental details:\n\n" +
                                    $"**Customer ID:** {customer.CustomerId}\n" +
                                    $"**Movie Title:** {movie.Title}\n" +
                                    $"**Rental Date:** {rental.RentalDate}\n" +
                                    $"**Due Date:** {rental.DueDate}\n" +
                                    $"**Rental Fee:** {rentalFeeFormatted}\n" +
                                    $"**Rental Status:** Pending";

                _logger.LogInformation("Successfully rented movie with ID: " + addRental.RentalId);


                SendMail("Complete Payment for Rent Movie", emailBody);

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
            await CheckAndUpdateOverdueRentals();

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
