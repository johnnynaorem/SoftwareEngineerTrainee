using System.ComponentModel.DataAnnotations;

namespace MovieRentWebAPI.Models.DTOs
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "User Name required")]
        [MinLength(5, ErrorMessage = "Length should be more than 4")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Enter Valid Email")]
        public string UserEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password required")]
        [MinLength(6, ErrorMessage = "Length should be more than or equal to 6")]
        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; }
    }
}
