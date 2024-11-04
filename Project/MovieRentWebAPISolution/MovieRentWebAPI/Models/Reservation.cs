namespace MovieRentWebAPI.Models
{
    public enum ReservationStatus
    {
        Active,
        Pending,
        Completed,
        Cancelled,
        Expired,
        Not_Available
    }
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public DateTime ReservationDate { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        // Navigation properties
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
