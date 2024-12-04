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
        public async Task<IEnumerable<RentalWithMovieAndCustomerDetailsDTO>> GetRentals()
        {
            var rentals = await _rentalRepo.GetAll();
            var rentalDtos = new List<RentalWithMovieAndCustomerDetailsDTO>();

            foreach (var rental in rentals)
            {
                var movie = await _movieRepo.Get(rental.MovieId);
                var customer = await _customerRepo.Get(rental.CustomerId);

                var rentalDto = new RentalWithMovieAndCustomerDetailsDTO
                {
                    RentalId = rental.RentalId,
                    RentalDate = rental.RentalDate,
                    DueDate = rental.DueDate,
                    ReturnDate = rental.ReturnDate,
                    Status = rental.Status.ToString(),
                    RentalFee = rental.RentalFee,

                    // Mapping Customer Details to CustomerDTO
                    Customer = new CustomerDTO
                    {
                        CustomerId = customer.CustomerId,
                        FullName = customer.FullName,
                        Email = customer.Email,
                        Address = customer.Address,
                        PhoneNumber = customer.PhoneNumber,
                    },

                    // Mapping Movie Details to MovieDTO
                    Movie = new MovieDetailsDTO
                    {
                        MovieId = movie.MovieId,
                        Title = movie.Title,
                        Genre = movie.Genre,
                        RentalPrice = movie.Rental_Price,
                        CoverImage = movie.CoverImage,
                        Description = movie.Description,
                        Rating = movie.Rating,
                        ReleaseDate = movie.ReleaseDate,
                        AvailableCopies = movie.AvailableCopies
                    }
                };

                rentalDtos.Add(rentalDto);
            }

            return rentalDtos.OrderByDescending(r => r.RentalId);
        }

        private async Task<Movie> GetMovie(int id)
        {
            var movie = await _movieRepo.Get(id);
            return movie;
        }

        private async Task<bool> IsReservedMovie(int movieId, int customerId)
        {
            var reservations = await _movieRervationService.GetAll();
            //var isMovieReserved = (
            //                        from reserve in reservations
            //                        where reserve.MovieId == movieId && reserve.Status == ReservationStatus.Active && reserve.CustomerId == customerId
            //                        select reserve.MovieId
            //                       ).ToList();
            //if (!isMovieReserved.Any()) return false;


            var isMovieReserved = reservations.FirstOrDefault(reserve => reserve.Movie.MovieId == movieId && reserve.Status == ReservationStatus.Active && reserve.CustomerId == customerId);


            if (isMovieReserved == null) return false;

            await _movieRervationService.UpdateMovieReservationStatus(new ReservedMovieStatusUpdateRequestDTO
            {
                ReservationId = isMovieReserved.ReservationId,
                Status = ReservationStatus.Completed
            });
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
        public async Task<int> RentMovie(RentMovieDTO rentMovieDTO)
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
                if (customer == null)
                {
                    throw new InvalidOperationException("Customer not Found");
                }

                bool isMovieReserved = await IsReservedMovie(movie.MovieId, customer.CustomerId);
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

                //return "Successfully rented movie with ID: " + addRental.RentalId.ToString();
                return addRental.RentalId;
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

        public async Task<IEnumerable<RentalWithMovieAndCustomerDetailsDTO>> GetRentalByCustomer(int customerId)
        {
            var rentals = await _rentalRepo.GetAll();
            var rentalsByCustomer = rentals.Where(r => r.CustomerId == customerId).ToList();

            var rentalDtos = new List<RentalWithMovieAndCustomerDetailsDTO>();

            foreach (var rental in rentalsByCustomer)
            {
                var movie = await _movieRepo.Get(rental.MovieId);
                var customer = await _customerRepo.Get(rental.CustomerId);

                var rentalDto = new RentalWithMovieAndCustomerDetailsDTO
                {
                    RentalId = rental.RentalId,
                    RentalDate = rental.RentalDate,
                    DueDate = rental.DueDate,
                    ReturnDate = rental.ReturnDate,
                    Status = rental.Status.ToString(),
                    RentalFee = rental.RentalFee,

                    // Mapping Customer Details to CustomerDTO
                    Customer = new CustomerDTO
                    {
                        CustomerId = customer.CustomerId,
                        FullName = customer.FullName,
                        Email = customer.Email,
                        Address = customer.Address,
                        PhoneNumber = customer.PhoneNumber,
                    },

                    // Mapping Movie Details to MovieDTO
                    Movie = new MovieDetailsDTO
                    {
                        MovieId = movie.MovieId,
                        Title = movie.Title,
                        Genre = movie.Genre,
                        RentalPrice = movie.Rental_Price,
                        CoverImage = movie.CoverImage,
                        Description = movie.Description,
                        Rating = movie.Rating,
                        ReleaseDate = movie.ReleaseDate,
                        AvailableCopies = movie.AvailableCopies
                    }
                };

                rentalDtos.Add(rentalDto);
            }

            return rentalDtos.OrderByDescending(r => r.RentalId);
        }
        public async Task<Rental> GetRentalByMovieIdAndCustomerId(int movieId, int customerId)
        {
            var rentals = await _rentalRepo.GetAll();
            var rental = rentals.FirstOrDefault(r => r.MovieId == movieId && r.CustomerId == customerId);
            return rental;
        }
    }

}
