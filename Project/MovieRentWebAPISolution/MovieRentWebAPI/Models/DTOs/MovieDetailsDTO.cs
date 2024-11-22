namespace MovieRentWebAPI.Models.DTOs
{
    public class MovieDetailsDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public double RentalPrice { get; set; }
        public string CoverImage { get; set; }
        public double? Rating { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AvailableCopies { get; set; }
    }
}
