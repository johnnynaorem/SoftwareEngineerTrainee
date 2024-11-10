namespace MovieRentWebAPI.Models
{
    public class ReviewForMovie
    {
        public int Id { get; set; }
        public string Comment {  get; set; } = string.Empty;
        public double? Rating { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
        public Movie Movie { get; set; } //Navigation 
        public Customer Customer { get; set; } //Navigation


    }
}
