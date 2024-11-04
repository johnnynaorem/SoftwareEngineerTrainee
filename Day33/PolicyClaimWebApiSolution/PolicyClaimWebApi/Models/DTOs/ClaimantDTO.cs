using PolicyClaimWebApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models.DTOs
{
    public class ClaimantDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone {  get; set; } = string.Empty;
    }
}
