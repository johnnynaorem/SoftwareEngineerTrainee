using PolicyClaimWebApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models.DTOs
{
    public class PolicyDTO
    {
        [PolicyNumberValidator]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "About Policy can not be blank")]
        public string AboutPolicy { get; set; } = string.Empty;
    }
}
