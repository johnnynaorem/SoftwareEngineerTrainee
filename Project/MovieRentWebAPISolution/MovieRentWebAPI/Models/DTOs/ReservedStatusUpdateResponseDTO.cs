namespace MovieRentWebAPI.Models.DTOs
{
    public class ReservedStatusUpdateResponseDTO
    {
        public int ReservationId { get; set; }
        public ReservationStatus Status { get; set; }
    }
}
