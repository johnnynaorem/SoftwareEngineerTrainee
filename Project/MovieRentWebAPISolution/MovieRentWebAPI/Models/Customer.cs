namespace MovieRentWebAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int UserId { get; set; }

        //navigate
        public User User { get; set; }
        public IEnumerable<Payment> Payments { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; }

    }
}
