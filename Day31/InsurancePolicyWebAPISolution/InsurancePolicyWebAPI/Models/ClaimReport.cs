using System.ComponentModel.DataAnnotations;

namespace InsurancePolicyWebAPI.Models
{
    public class ClaimReport
    {
        public int Id { get; set; }
        public int ClaimId { get; set; } 
        public string ReportDetails { get; set; } = string.Empty;
        public string FilePath { get; set; }
        public Claim Claim { get; set; }
    }
}
