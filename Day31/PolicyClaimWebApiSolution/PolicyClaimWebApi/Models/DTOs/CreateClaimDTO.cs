using System.ComponentModel.DataAnnotations;
using PolicyClaimWebApi.Validations;
namespace PolicyClaimWebApi.Models.DTOs
{
    public class CreateClaimDTO
    {
        [PolicyNumberValidator]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type can not be blank")]
        public string ClaimType { get; set; } = string.Empty;

        [Required(ErrorMessage = "ClaimantName can not be blank")]
        public string ClaimantName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ClaimantPhone can not be blank")]
        public string ClaimantPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "ClaimantEmail can not be blank")]
        public string ClaimantEmail { get; set; } = string.Empty;


        [Required(ErrorMessage = "ClaimDate can not be blank")]
        public DateTime ClaimDate { get; set; }
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
