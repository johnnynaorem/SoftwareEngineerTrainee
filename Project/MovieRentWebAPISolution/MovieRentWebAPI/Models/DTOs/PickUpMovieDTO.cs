namespace MovieRentWebAPI.Models.DTOs
{
    public class PickUpMovieDTO
    {
        public int RentId { get; set; } 
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
    }
}
