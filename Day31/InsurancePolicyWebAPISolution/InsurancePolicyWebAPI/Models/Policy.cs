using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyWebAPI.Models
{
    public class Policy
    {
        [Key]
        public string PolicyNumber { get; set; } = string.Empty;
        public string PolicyDetail { get; set; } = string.Empty ;
        //public int ClaimantId { get; set; }
        //public Claimant Claimant { get; set; }
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
