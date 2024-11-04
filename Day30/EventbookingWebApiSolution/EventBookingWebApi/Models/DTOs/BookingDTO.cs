namespace EventBookingWebApi.Models.DTOs
{
    public class BookingDTO
    {
        public int EmployeeId { get; set; }
        public int EventId { get; set; }
        public DateTime BookingTime { get; set; } = DateTime.Now;
    }
}
