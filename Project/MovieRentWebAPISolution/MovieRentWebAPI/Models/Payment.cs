namespace MovieRentWebAPI.Models
{
    public class Payment
    {
        public int paymentId { get; set; }
        public int CustomerId { get; set; }
        public int RentalId {  get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string PaymentType { get; set; }

        //Navigate
        public Rental Rental { get; set; }
        public Customer Customer { get; set; }
    }
}
