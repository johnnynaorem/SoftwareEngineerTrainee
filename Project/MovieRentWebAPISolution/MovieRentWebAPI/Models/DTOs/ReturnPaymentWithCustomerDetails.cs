namespace MovieRentWebAPI.Models.DTOs
{
    public class ReturnPaymentWithCustomerDetails
    {
        public int paymentId { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string PaymentType { get; set; }
        public int RentalId { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
