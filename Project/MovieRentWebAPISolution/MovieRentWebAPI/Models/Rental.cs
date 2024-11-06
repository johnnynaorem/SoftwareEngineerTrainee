namespace MovieRentWebAPI.Models
{
    public enum RentalStatus {
        Pending, Confirmed, Active, Returned, Overdue, Cancelled
    }
    public class Rental
    {
        public int RentalId { get; set; }
        public DateTime RentalDate {  get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Pending;
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public double RentalFee { get; set; }

        //Navigate
        public Payment Payment {  get; set; }
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
