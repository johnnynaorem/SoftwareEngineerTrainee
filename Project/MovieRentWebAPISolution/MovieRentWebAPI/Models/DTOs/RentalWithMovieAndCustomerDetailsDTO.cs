namespace MovieRentWebAPI.Models.DTOs
{
    public class RentalWithMovieAndCustomerDetailsDTO
    {
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
        public double RentalFee { get; set; }
        public CustomerDTO Customer { get; set; }
        public MovieDetailsDTO Movie { get; set; }
    }
}
