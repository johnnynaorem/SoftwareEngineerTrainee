namespace EventBookingWebApi.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
