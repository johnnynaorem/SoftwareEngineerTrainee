using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IPaymentService
    {
        Task <int> GeneratePayment(MakePaymentDTO paymentCredentials);
        Task<IEnumerable<Payment>> GetAllPayments();
    }
}
