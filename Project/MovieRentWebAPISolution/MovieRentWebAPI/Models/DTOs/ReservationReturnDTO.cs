namespace MovieRentWebAPI.Models.DTOs
{
    public class ReservationReturnDTO
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string Status { get; set; }
        public CustomerResponseDTO Customer { get; set; }
        public MovieResponseDTO Movie { get; set; }
    }
}
