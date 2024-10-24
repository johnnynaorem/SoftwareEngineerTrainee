namespace InsurancePolicyWebAPI.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int ClaimTypeId { get; set; } 
        public ClaimType ClaimType { get; set; }

        public int ClaimantId { get; set; } 
        public Claimant Claimant { get; set; }
        public DateTime DateOfIncident { get; set; }
        public string PolicyNumber { get; set; }
        public Policy Policy { get; set; }
        public ICollection<ClaimReport> ClaimReports { get; set; } = new List<ClaimReport>();
    }
}
