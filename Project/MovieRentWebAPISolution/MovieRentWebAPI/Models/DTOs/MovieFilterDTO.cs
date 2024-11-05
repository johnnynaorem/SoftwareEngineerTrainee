namespace MovieRentWebAPI.Models.DTOs
{
    public class MovieFilterDTO
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double? MinimumRating { get; set; }
    }
}