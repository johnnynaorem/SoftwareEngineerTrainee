namespace MovieRentWebAPI.Models.DTOs
{
    public class RentalUpdateRequestDTO
    {
        public int RentalId { get; set; }
        public RentalStatus Status { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
