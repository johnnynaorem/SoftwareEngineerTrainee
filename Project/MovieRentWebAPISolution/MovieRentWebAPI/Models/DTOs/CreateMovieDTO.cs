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

        [RegularExpression(@"^(19|20)\d{2}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Enter the valid format \"YYYY-MM-DD\"")]
        public DateTime ReleaseDate { get; set; }
    }
}
