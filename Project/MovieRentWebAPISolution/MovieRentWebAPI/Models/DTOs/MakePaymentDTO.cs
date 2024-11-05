namespace MovieRentWebAPI.Models.DTOs
{
    public class MakePaymentDTO
    {
        public int CustomerId { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string PaymentType { get; set; }
    }
}
