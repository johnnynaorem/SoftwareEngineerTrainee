using System.ComponentModel.DataAnnotations;

namespace MovieRentWebAPI.Models.DTOs
{
    public class CreateMovieDTO
    {
        [Required(ErrorMessage = "Cannot be Blank")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cannot be Blank")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cannot be Blank")]
        public double Rental_Price { get; set; }

        [Required(ErrorMessage = "Cannot be Blank")]
        public string CoverImage { get; set; } = string.Empty;

        public double Rating { get; set; }

        [Required(ErrorMessage = "Cannot be Blank")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cannot be Blank")]
        public int AvailableCopies { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
