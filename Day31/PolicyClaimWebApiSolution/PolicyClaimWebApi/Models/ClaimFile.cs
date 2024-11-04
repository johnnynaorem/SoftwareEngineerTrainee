namespace PolicyClaimWebApi.Models
{
    public class ClaimFile
    {
        public int ClaimFileId { get; set; }
        public int ClaimID { get; set; } 
        public string FilePath { get; set; } = string.Empty;

        public Claim Claim { get; set; } 
    }
}
