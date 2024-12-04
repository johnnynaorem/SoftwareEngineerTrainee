namespace MovieRentWebAPI.Models.DTOs
{
    public class ResponseReviewsWithCustomerDetailsDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public double? Rating { get; set; }
        public CustomerResponseDTO Customer { get; set; }
    }
}
