namespace MovieRentWebAPI.Models.DTOs
{
    public class PickUpResponseMovieDTO
    {
        public int RentalId { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public RentalStatus Status { get; set; }
    }
}
