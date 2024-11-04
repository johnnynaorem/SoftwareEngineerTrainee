namespace MovieRentWebAPI.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }

        //Navigate
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
