namespace MovieRentWebAPI.Models.DTOs
{
    public class MakePaymentDTO
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public string PaymentType { get; set; }
    }
}
