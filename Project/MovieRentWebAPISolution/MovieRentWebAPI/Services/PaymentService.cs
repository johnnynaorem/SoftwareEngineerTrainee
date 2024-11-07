using MovieRentWebAPI.EmailInterface;
using MovieRentWebAPI.EmailModels;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;
using MovieRentWebAPI.Repositories;

namespace MovieRentWebAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IRepository<int, Payment> _paymentRepo;
        private readonly IRepository<int, Rental> _rentalRepo;
        private readonly IRepository<int, Customer> _customerRepo;
        private readonly IRepository<int, Movie> _movieRepo;
        private IRentalService _rentalService;
        private IEmailSender _emailSender;

        public PaymentService(IRepository<int, Payment> paymentRepository, IRepository<int, Rental> rentalRepo, ILogger<PaymentService> logger, IRentalService rentalService, IEmailSender emailSender, IRepository<int, Customer> customerRepository, IRepository<int, Movie> movieRepository)
        {
            _logger = logger;
            _paymentRepo = paymentRepository;
            _rentalRepo = rentalRepo;
            _rentalService = rentalService;
            _emailSender = emailSender;
            _customerRepo = customerRepository;
            _movieRepo = movieRepository;
        }

        private async Task<Rental> GetRental(int id) {
            var rental = await _rentalRepo.Get(id);
            if (rental != null) { 
                return rental;
            }
            throw new InvalidOperationException($"Not Rental Found with ID {id}");
        }

        private async Task SendMail(string title, string body)
        {
            var rng = new Random();
            var message = new Message(new string[] {
                        "johnnynaorem7@gmail.com" },
                    title,
                    body);
            _emailSender.SendEmail(message);
        }

        public async Task<string> GeneratePayment(MakePaymentDTO payment)
        {
            try
            {
                var rental = await GetRental(payment.RentalId);

                var paymentCredentials = new Payment
                {
                    RentalId = rental.RentalId,
                    CustomerId = payment.CustomerId,
                    PaymentType = payment.PaymentType,
                    Amount = rental.RentalFee,
                    PaymentDate = DateTime.Now,
                };

                var addPayment = await _paymentRepo.Add(paymentCredentials);

                var updateRentalStatus = new RentalUpdateRequestDTO
                {
                    RentalId = payment.RentalId,
                    Status = RentalStatus.Confirmed,
                };

                var customer = await _customerRepo.Get(payment.CustomerId);
                var movie = await _movieRepo.Get(rental.MovieId);

                await _rentalService.Update(updateRentalStatus);

                string emailBody = $"Dear {customer.FullName},\n\n" +
                                    "Your movie rental has been successfully created!\n\n" +
                                    $"We are happy to inform you that your rental for the movie **{movie.Title}** is now **Active** after Payment Completed\n\n" +
                                    "Here are your rental details:\n\n" +
                                    $"**Customer ID:** {customer.CustomerId}\n" +
                                    $"**Movie Title:** {movie.Title}\n" +
                                    $"**Rental Date:** {rental.RentalDate}\n" +
                                    $"**Due Date:** {rental.DueDate}\n" +
                                    $"**Rental Fee:** {rental.RentalFee:C}\n" +
                                    $"**Rental Status:** Active";

                SendMail("Your Rental Status Updated To Active", emailBody);

                return $"Payment is Successfull.\nYour PaymentId is {addPayment.paymentId} with Payment Method: {addPayment.PaymentType}";
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                //throw new Exception(ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            var payment = await _paymentRepo.GetAll();
            return payment;
        }
    }
}
