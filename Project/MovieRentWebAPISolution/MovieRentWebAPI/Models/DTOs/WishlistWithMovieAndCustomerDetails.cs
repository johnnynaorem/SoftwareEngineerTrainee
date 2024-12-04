namespace MovieRentWebAPI.Models.DTOs
{
    public class WishlistWithMovieAndCustomerDetails
    {
        public int WishlistId { get; set; }
        public MovieDetailsDTO Movie { get; set; }
        public CustomerResponseDTO Customer { get; set; }
    }
}
