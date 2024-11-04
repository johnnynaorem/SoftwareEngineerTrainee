namespace MovieRentWebAPI.Models
{
    public enum RentalStatus {
        Active,
        Return,
        Overdue
    }
    public class Rental
    {
        public int RentalId { get; set; }
        public DateTime RentalDate {  get; set; }
        public DateTime ReturnDate { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Active;
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public double RentalFee { get; set; }

        //Navigate
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
