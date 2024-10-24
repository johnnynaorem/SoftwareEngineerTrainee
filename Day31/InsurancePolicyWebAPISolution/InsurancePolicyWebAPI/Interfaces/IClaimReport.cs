using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Models.DTOs;

namespace InsurancePolicyWebAPI.Interfaces
{
    public interface IClaimReport
    {
        Task<ClaimReport> UploadFileAsync(IFormFile file, ClaimReportRequest request);
    }
}
