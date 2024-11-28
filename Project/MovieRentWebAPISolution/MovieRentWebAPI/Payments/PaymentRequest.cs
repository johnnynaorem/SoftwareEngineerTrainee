using static MovieRentWebAPI.Controllers.PaymentController;

namespace MovieRentWebAPI.Payments
{
    public class PaymentRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public Address CustomerAddress { get; set; }
    }
}
