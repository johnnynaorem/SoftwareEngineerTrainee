namespace PolicyClaimWebApi.Models.DTOs
{
    public class UpdateClaimStatusDTO
    {
        public int ClaimID { get; set; }
        public Status Status { get; set; }
    }
}
