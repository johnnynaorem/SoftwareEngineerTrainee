namespace EventBookingWebApi.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public DateTime BookingTime { get; set; } = DateTime.Now;
    }
}
