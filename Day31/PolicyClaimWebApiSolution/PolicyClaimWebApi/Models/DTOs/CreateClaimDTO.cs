using System.ComponentModel.DataAnnotations;
using PolicyClaimWebApi.Validations;
namespace PolicyClaimWebApi.Models.DTOs
{
    public class CreateClaimDTO
    {
        [PolicyNumberValidator]
        public string PolicyNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "ClaimantId can not be blank")]
        public int ClaimantId { get; set; }
        [Required(ErrorMessage = "ClaimDate can not be blank")]
        public DateTime ClaimDate { get; set; }
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
}
