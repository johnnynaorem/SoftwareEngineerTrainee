using System.ComponentModel.DataAnnotations;

namespace MovieRentWebAPI.Models.DTOs
{
    public class ReservedMovieStatusUpdateRequestDTO
    {
        [Required(ErrorMessage = "Required!!!! Cannot be blank")]
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Required!!!! Cannot be blank")]
        public ReservationStatus Status { get; set; }
    }
}
