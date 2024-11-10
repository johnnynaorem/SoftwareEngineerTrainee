namespace MovieRentWebAPI.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public double Rental_Price { get; set; }
        public string CoverImage {  get; set; } = string.Empty;
        public double? Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate {  get; set; } = DateTime.Now;
        //public string Author { get; set; } = string.Empty;
        public int AvailableCopies { get; set; }

        //Navigate
        public IEnumerable<Rental> Rentals { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public IEnumerable<ReviewForMovie> Reviews { get; set; }
    }
}
