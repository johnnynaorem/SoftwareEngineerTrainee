using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<Payment> _logger;
        private readonly IRepository<int, Payment> _paymentRepo;
        private readonly IRepository<int, Movie> _moiveRepo;

        public PaymentService(IRepository<int, Payment> paymentRepository, IRepository<int, Movie> movieRepo, ILogger<Payment> logger)
        {
            _logger = logger;
            _paymentRepo = paymentRepository;
            _moiveRepo = movieRepo;
        }
        public async Task<int> GeneratePayment(MakePaymentDTO payment)
        {
            var paymentCredentials = new Payment
            {
                CustomerId = payment.CustomerId,
                Amount = payment.Amount,
                PaymentType = payment.PaymentType
            };

            var addPayment = await _paymentRepo.Add(paymentCredentials);
            return addPayment.paymentId;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            var payment = await _paymentRepo.GetAll();
            return payment;
        }
    }
}
