using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<Payment> _logger;
        private readonly IRepository<int, Payment> _paymentRepo;
        private readonly IRepository<int, Rental> _rentalRepo;
        private IRentalService _rentalService;

        public PaymentService(IRepository<int, Payment> paymentRepository, IRepository<int, Rental> rentalRepo, ILogger<Payment> logger, IRentalService rentalService)
        {
            _logger = logger;
            _paymentRepo = paymentRepository;
            _rentalRepo = rentalRepo;
            _rentalService = rentalService;
        }

        private async Task<Rental> GetRental(int id) {
            var rental = await _rentalRepo.Get(id);
            if (rental != null) { 
                return rental;
            }
            throw new InvalidOperationException($"Not Rental Found with ID {id}");
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

                await _rentalService.Update(updateRentalStatus);
                return $"Payment is Successfull.\nYour PaymentId is {addPayment.paymentId} with Payment Method: {addPayment.PaymentType}";
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            var payment = await _paymentRepo.GetAll();
            return payment;
        }
    }
}
