using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyWebAPI.Models
{
    public class Claimant
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<Claim> Claims { get; set; }
        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
    }   
}
