﻿using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Context;
using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.EmailModels;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using System.Globalization;

namespace MovieRentWebAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, Customer> _customerRepo;
        private readonly ILogger<CustomerService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IRentalService _rentalService;
        private readonly IRepository<int, Movie> _movieRepo;
        private readonly MovieRentContext _context;
        private readonly IRepository<int, ReviewForMovie> _reviewRepo;

        public CustomerService(IRepository<int, Customer> customerRepository, IRepository<int, ReviewForMovie> reviewRepo, ILogger<CustomerService> logger, IEmailSender emailSender, IRentalService rentalService, IRepository<int, Movie> movieRepo, MovieRentContext context)
        {

            _rentalService = rentalService;
            _customerRepo = customerRepository;
            _logger = logger;
            _emailSender = emailSender;
            _movieRepo = movieRepo;
            _context = context;
            _reviewRepo = reviewRepo;
        }

        private void SendMail(string mailTo, string title, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        mailTo },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }
        public async Task<int> CreateCustomer(CreateCustomerDTO entity)
        {
            var isValidUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == entity.UserId);
            if (isValidUser == null)
            {
                throw new InvalidOperationException($"Invalid User, there is no user with userId: {entity.UserId} in database");
            }
            var customer = new Customer
            {
                FullName = entity.FullName,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                UserId = entity.UserId,
                Email = isValidUser.UserEmail,
            };

            var addCustomer = await _customerRepo.Add(customer);
            if (addCustomer != null)
            {
                string emailBody =
                                $"Dear {addCustomer.FullName},\n\n" +
                                "Thank you for registering with the Video Disc Rental App!\n\n" +
                                $"Your customer account has been successfully created at {DateTime.Now}.\n\n" +
                                "Here are your account details:\n\n" +
                                $"**Customer ID:** {addCustomer.CustomerId}\n" +
                                $"**Registered Contact Number:** {addCustomer.PhoneNumber}\n\n" +
                                "You can now start browsing our extensive catalog of movies, reserve discs, and manage your rentals. To log in to your account, please click the link below:\n\n" +
                                $"[Login to Your Account](https://localhost:7203/swagger/index.html)\n\n" +
                                "For your convenience, here are some tips to enhance your experience:\n" +
                                "- Explore different genres to find your favorites.\n" +
                                "- Use our wishlist feature to save movies for later.\n" +
                                "- Don’t hesitate to reach out if you need assistance with your account or rentals.\n\n" +
                                "Thank you for choosing us! If you have any questions, feel free to contact our support team.\n\n" +
                                "Best regards,\n" +
                                "The Video Disc Rental App Team";

                SendMail(isValidUser.UserEmail, "Your Customer Account Has Been Created", emailBody);
            }
            return addCustomer.CustomerId;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            var customers = await _customerRepo.GetAll();
            return customers;
        }

        public Task<Customer> GetCustomerById(int id)
        {
            var customer = _customerRepo.Get(id);
            return customer;
        }

        public async Task<int> UpdateCustomer(CreateCustomerDTO customer, int key)
        {
            var updateCustomer = new Customer
            {
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };

            var updatedCustomer = await _customerRepo.Update(updateCustomer, key);
            if (updateCustomer != null)
            {
                string emailBody =
                                $"Dear {updatedCustomer.FullName},\n\n" +
                                "Your profile has been successfully updated in the Video Disc Rental App!\n\n" +
                                $"The changes were made on {DateTime.Now}.\n\n" +
                                "Here are your updated account details:\n\n" +
                                $"**Customer ID:** {updatedCustomer.CustomerId}\n" +
                                $"**Updated Contact Number:** {updatedCustomer.PhoneNumber}\n\n" +
                                "You can continue to browse our extensive catalog of movies, reserve discs, and manage your rentals. If you need to log in to your account again, please click the link below:\n\n" +
                                $"[Log in to Your Account](www.MovieRentalApp.com)\n\n" +
                                "To enhance your experience, here are some tips:\n" +
                                "- Explore different genres to discover new favorites.\n" +
                                "- Use our wishlist feature to save movies for later viewing.\n" +
                                "- Reach out to us if you need assistance with your account or rentals.\n\n" +
                                "Thank you for being a valued customer! If you have any questions or need further assistance, feel free to contact our support team.\n\n" +
                                "Best regards,\n" +
                                "The Video Disc Rental App Team";

                SendMail(updatedCustomer.Email, "Your Customer Account Profile Has Been Updated", emailBody);
            }
            return updatedCustomer.CustomerId;
        }

        private async Task<bool> IsMovieReadyToPickUp(int movieId, int customerId)
        {
            var rentals = await _rentalService.GetRentals();
            var isReady = (
                            from rental in rentals
                            where rental.Movie.MovieId == movieId && rental.Customer.CustomerId == customerId && rental.Status == "Confirmed"
                            select new { rental.Movie.MovieId, rental.Customer.CustomerId }
                           ).ToList();

            var movie = await _movieRepo.Get(movieId);

            if (isReady.Any() && movie.AvailableCopies > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<PickUpResponseMovieDTO> PickUpMovie(PickUpMovieDTO pickUpMovie)
        {
            bool isReady = await IsMovieReadyToPickUp(pickUpMovie.MovieId, pickUpMovie.CustomerId);
            if (!isReady)
            {
                throw new MoviePickUpException("Cannot Pick Up Movie, Because The Movie is not in Reserved by You or Your Rental status is not Completed");
            }

            var customer = await _customerRepo.Get(pickUpMovie.CustomerId);
            var movie = await _movieRepo.Get(pickUpMovie.MovieId);
            var rentals = await _rentalService.GetRentals();
            var rental = rentals.FirstOrDefault(r => r.RentalId == pickUpMovie.RentId);

            var rentalUpdate = new RentalUpdateRequestDTO
            {
                Status = RentalStatus.Active,
                RentalId = pickUpMovie.RentId,
            };
            //Movie Available Quantity

            await _movieRepo.Update(new Movie { AvailableCopies = movie.AvailableCopies - 1 }, movie.MovieId);
            var updateStatusRental = await _rentalService.Update(rentalUpdate);
            var rentalFeeFormatted = rental.RentalFee.ToString("C", new CultureInfo("en-IN"));
            string emailBody =
                                    $"Dear {customer.FullName},\n\n" +
                                    "Your movie rental status has been successfully updated!\n\n" +
                                    $"We are happy to inform you that your rental for the movie **{movie.Title}** is now **Active** after you picked it up from our store.\n\n" +
                                    "Here are your updated rental details:\n\n" +
                                    $"**Customer ID:** {customer.CustomerId}\n" +
                                    $"**Movie Title:** {movie.Title}\n" +
                                    $"**Rental Date:** {rental.RentalDate}\n" +
                                    $"**Due Date:** {rental.DueDate}\n" +
                                    $"**Rental Fee:** {rentalFeeFormatted}\n" +
                                    $"**Rental Status:** Active";

            SendMail(customer.Email, "Your Rental Status Updated To Active", emailBody);
            return new PickUpResponseMovieDTO
            {
                RentalId = updateStatusRental.RentalId,
                MovieId = updateStatusRental.MovieId,
                CustomerId = updateStatusRental.CustomerId,
                Status = updateStatusRental.Status,
            };
        }

        public async Task<ReturnMovieResponseDTO> ReturnMovie(ReturnMovieResquestDTO returnMovie)
        {
            try
            {
                var rentals = await _rentalService.GetRentals();

                var rental = rentals.FirstOrDefault(r => r.RentalId == returnMovie.RentId && r.Customer.CustomerId == returnMovie.CustomerId);
                if (rental == null) throw new InvalidOperationException($"There is no Rental With Rental ID: {returnMovie.RentId}");

                var rentalUpdate = new RentalUpdateRequestDTO
                {
                    RentalId = returnMovie.RentId,
                    ReturnDate = DateTime.Now,
                    Status = RentalStatus.Returned
                };
                var movie = await _movieRepo.Get(rental.Movie.MovieId);
                var customer = await _customerRepo.Get(rental.Customer.CustomerId);
                var returnMovieAndUpdateRentalStatusToReturn = await _rentalService.Update(rentalUpdate);
                if (returnMovieAndUpdateRentalStatusToReturn != null)
                {
                    var rentalFeeFormatted = rental.RentalFee.ToString("C", new CultureInfo("en-IN"));
                    string emailBody =
                                    $"Dear {customer.FullName},\n\n" +
                                    "Your movie rental status has been successfully updated!\n\n" +
                                    $"We are happy to inform you that your rental for the movie **{movie.Title}** is now **Returned** after you returned it to our store.\n\n" +
                                    "Here are your updated rental details:\n\n" +
                                    $"**Customer ID:** {returnMovieAndUpdateRentalStatusToReturn.CustomerId}\n" +
                                    $"**Movie Title:** {movie.Title}\n" +
                                    $"**Rental Date:** {rental.RentalDate}\n" +
                                    $"**Due Date:** {rental.DueDate}\n" +
                                    $"**Rental Fee:** {rentalFeeFormatted}\n" +
                                    $"**Rental Status:** Returned";


                    SendMail(customer.Email, "Successfully Return Movie", emailBody);
                }
                return new ReturnMovieResponseDTO
                {
                    Message = $"Successfully Return Movie, MovieID: {returnMovieAndUpdateRentalStatusToReturn.MovieId}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while Returning movie.");
                throw;
            }
        }

        public async Task<int> CommentToMovie(MakeCommentDTO comment)
        {
            var addComment = new ReviewForMovie
            {
                Comment = comment.Comment,
                Rating = comment.Rating,
                CustomerId = comment.CustomerId,
                MovieId = comment.MovieId
            };
            var response = await _reviewRepo.Add(addComment);
            return response.Id;
        }

        public async Task<IEnumerable<ResponseReviewsWithCustomerDetailsDTO>> GetAllReviewForMovie(int movieId)
        {
            var reviews = await _reviewRepo.GetAll();
            var returnReviews = reviews.Where(r => r.MovieId == movieId).OrderByDescending(o => o.Id).ToList();
            var result = new List<ResponseReviewsWithCustomerDetailsDTO>().ToList();
            foreach (var item in returnReviews)
            {
                var customer = await _customerRepo.Get(item.CustomerId);
                var comments = new ResponseReviewsWithCustomerDetailsDTO
                {
                    Id = item.Id,
                    Rating = item.Rating,
                    Comment = item.Comment,
                    Customer = new CustomerResponseDTO
                    {
                        FullName = customer.FullName,
                        PhoneNumber = customer.PhoneNumber,
                        Address = customer.Address,
                        Email = customer.Email,
                    }
                };
                result.Add(comments);
            }
            return result;
        }

    }
}
