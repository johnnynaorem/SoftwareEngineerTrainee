using System.ComponentModel.DataAnnotations;

namespace MovieRentWebAPI.Models.DTOs
{
    public class ReserveMovieDTO
    {
        [Required(ErrorMessage = "CustomerId Cannot be blank")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "MovieId Cannot be blank")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "ReservationDate Cannot be blank")]
        public DateTime ReservationDate { get; set; }
    }
}
