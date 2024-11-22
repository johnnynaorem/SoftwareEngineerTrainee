namespace MovieRentWebAPI.Models.DTOs
{
    public class MovieResponseDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
    }
}
