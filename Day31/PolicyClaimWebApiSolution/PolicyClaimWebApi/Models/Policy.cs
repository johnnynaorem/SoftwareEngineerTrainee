using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models
{
    public class Policy
    {
        [Key]
        public string PolicyNumber { get; set; }
        public string AboutPolicy { get; set; } = string.Empty;

        public ICollection<ClaimType> Types { get; set; } = new List<ClaimType>();
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
