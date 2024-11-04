using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Models.DTOs;
using InsurancePolicyWebAPI.Repositories;
using System.IO;
namespace InsurancePolicyWebAPI.Services
{
    public class ClaimReportService : IClaimReport
    {
        private readonly string _fileStoragePath;
        private readonly IRepository<int, ClaimReport> _report;
        private readonly IRepository<int, Claim> _claimRepo;

        public ClaimReportService(IRepository<int, ClaimReport> repository, IRepository<int, Claim> claim_repository)
        {
            _fileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            _report = repository;
            _claimRepo = claim_repository;

            if (!Directory.Exists(_fileStoragePath))
            {
                Directory.CreateDirectory(_fileStoragePath);
            }
        }
        public async Task<ClaimReport> UploadFileAsync(IFormFile file, ClaimReportRequest request)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            var allowedExtensions = new[] { ".pdf", ".jpg", ".png" }; 
            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Invalid file type.");
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_fileStoragePath, uniqueFileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File upload failed.", ex);
            }

            Claim newClaim = new Claim
            {
                ClaimTypeId = request.ClaimTypeId,
                PolicyNumber = request.PolicyNumber,
                DateOfIncident = request.DateOfIncident,
                ClaimantId = request.ClaimantId,

            };

            var createClaim = await _claimRepo.Add(newClaim);

            ClaimReport newReport = new ClaimReport
            {
                ClaimId = createClaim.Id,
                //ReportDetails = reportDetails,  
                FilePath = filePath
            };

            _report.Add(newReport);
            return newReport; 
        }

    }
}
