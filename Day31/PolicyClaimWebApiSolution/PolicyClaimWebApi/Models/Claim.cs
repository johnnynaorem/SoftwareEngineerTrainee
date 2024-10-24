namespace PolicyClaimWebApi.Models
{
    public class Claim
    {
        public int ClaimID { get; set; }
        public string PolicyNumber { get; set; }
        public int ClaimantId { get; set; }
        public DateTime ClaimDate { get; set; }
      
        public IEnumerable<ClaimFile> ClaimFiles { get; set; }

        public Policy Policy { get; set; }
        public Claimant Claimant { get; set; }


    }
}
