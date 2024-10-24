using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyWebAPI.Models
{
    public class ClaimType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; 

        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
