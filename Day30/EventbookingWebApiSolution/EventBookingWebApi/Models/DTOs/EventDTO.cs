namespace EventBookingWebApi.Models.DTOs
{
    public class EventDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public int EmployeeId { get; set; }
    }
}
