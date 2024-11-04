using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models.DTOs
{
    public class ClaimTypeDTO
    {
        [Required(ErrorMessage = "Type Name cannot be blank")]
        public string TypeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type Description cannot be blank")]
        public string TypeDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Policy Number cannot be blank")]
        [MinLength(6, ErrorMessage = "Policy Number must be at least 6 characters long")]
        public string PolicyNumber { get; set; } = string.Empty;
    }
}
