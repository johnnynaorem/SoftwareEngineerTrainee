using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models
{
    public class Policy
    {
        [Key]
        public string PolicyNumber { get; set; }
        public string AboutPolicy { get; set; } = string.Empty;
        public IEnumerable<ClaimType> Types { get; set; } = new List<ClaimType>();
        public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();
    }
}
