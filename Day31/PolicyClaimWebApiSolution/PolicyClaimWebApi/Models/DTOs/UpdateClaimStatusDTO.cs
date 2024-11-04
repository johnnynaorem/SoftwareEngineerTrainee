namespace PolicyClaimWebApi.Models.DTOs
{
    public class UpdateClaimStatusDTO
    {
        public int ClaimId { get; set; }
        public Status Status { get; set; }
    }
}
