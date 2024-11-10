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
        public IEnumerable<Rental> Rentals { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public IEnumerable<Wishlist> Wishlists { get; set; }
        public IEnumerable<ReviewForMovie> Reviews { get; set; }

    }
}
