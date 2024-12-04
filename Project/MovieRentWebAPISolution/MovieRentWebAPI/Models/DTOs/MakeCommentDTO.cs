using System.ComponentModel.DataAnnotations;

namespace MovieRentWebAPI.Models.DTOs
{
    public class MakeCommentDTO
    {
        public string Comment { get; set; }
        public double? Rating { get; set; }
        public int MovieId { get; set; }
        public int CustomerId { get; set; }
    }
}
