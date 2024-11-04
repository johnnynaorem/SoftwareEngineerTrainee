using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models.DTOs
{
    public class UserDTO
    {
        [Required(ErrorMessage = "Username Should not be blank")]
        [MinLength(5,ErrorMessage = "Username length should be more than or eqaual to 5")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password Should not be blank")]
        [MinLength(5, ErrorMessage = "Username length should be more than or eqaual to 5")]
        public string Password { get; set; } = string.Empty;
        public Roles Role { get; set; }
    }
}
