using System.ComponentModel.DataAnnotations;

namespace PolicyClaimWebApi.Models
{
    public class ClaimType
    {
        [Key]
        public int TypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string TypeDescription { get; set; } = string.Empty;
        public string PolicyNumber { get; set; } = string.Empty;
        public Policy Policy { get; set; }
    }
}
