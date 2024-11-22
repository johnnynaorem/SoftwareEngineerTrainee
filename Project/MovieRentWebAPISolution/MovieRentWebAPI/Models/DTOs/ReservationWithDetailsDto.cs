namespace MovieRentWebAPI.Models.DTOs
{
    public class ReservationWithMovieAndCustomerDto
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public ReservationStatus Status { get; set; }

        // Customer Details
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomerEmail { get; set; }

        // Movie Details as a nested object
        public MovieDetailsDTO Movie { get; set; }
    }
}
