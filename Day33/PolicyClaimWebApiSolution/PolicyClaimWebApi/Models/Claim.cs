namespace PolicyClaimWebApi.Models
{
    public enum Status
    {
        Pending,
        Processing,
        Approved,
        Rejected
    }
    public class Claim
    {
        public int ClaimID { get; set; }
        public string PolicyNumber { get; set; }
        public int ClaimantId { get; set; } 
        public DateTime ClaimDate { get; set; }

        public string TypeName { get; set; }

        public Status Status { get; set; } = Status.Pending;

        public ClaimType ClaimType { get; set; } 

        public ICollection<ClaimFile> ClaimFiles { get; set; } = new List<ClaimFile>();
        public Policy Policy { get; set; } 
        public Claimant Claimant { get; set; }

    }
}
