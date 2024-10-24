namespace InsurancePolicyWebAPI.Models.DTOs
{
    public class ClaimReportRequest
    {
        public IFormFile FilePath { get; set; }
        public int ClaimTypeId { get; set; }
        public int ClaimantId { get; set; }
        public DateTime DateOfIncident { get; set; }
        public string PolicyNumber { get; set; }
    }
}
