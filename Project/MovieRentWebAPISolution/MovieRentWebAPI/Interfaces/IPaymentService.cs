using MovieRentWebAPI.Models;
using MovieRentWebAPI.Models.DTOs;

namespace MovieRentWebAPI.Interfaces
{
    public interface IPaymentService
    {
        Task <string> GeneratePayment(MakePaymentDTO paymentCredentials);
        Task<IEnumerable<Payment>> GetAllPayments();
    }
}
