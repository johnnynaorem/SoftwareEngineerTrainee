namespace InsurancePolicyWebAPI.Models.DTOs
{
    public class ClaimDTO
    {
        public int ClaimTypeId { get; set; }
        public int ClaimantId { get; set; }
        public DateTime DateOfIncident { get; set; }
        public string PolicyNumber { get; set; }
    }
}
