namespace MovieRentWebAPI.Models
{
    public class Payment
    {
        public int paymentId { get; set; }
        public int CustomerId { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }

        //Navigate
        public Customer Customer { get; set; }
    }
}
