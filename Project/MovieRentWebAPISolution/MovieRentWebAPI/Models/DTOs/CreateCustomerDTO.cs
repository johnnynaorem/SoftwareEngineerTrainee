using System.ComponentModel.DataAnnotations;

namespace MovieRentWebAPI.Models.DTOs
{
    public class CreateCustomerDTO
    {
        [Required(ErrorMessage = "Cannot be blank")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cannot be blank")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Enter Valid Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address Field Cannot be Blank")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cannot be Blank")]
        public int UserId { get; set; }
    }
}
