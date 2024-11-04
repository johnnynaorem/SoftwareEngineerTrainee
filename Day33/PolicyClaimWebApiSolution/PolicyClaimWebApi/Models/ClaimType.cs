using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models
{
    public class ClaimType
    {
        [Key]
        public string TypeName { get; set; } = string.Empty;
        public string TypeDescription { get; set; } = string.Empty;

        public string PolicyNumber { get; set; } 
        public Policy Policy { get; set; } 

        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
