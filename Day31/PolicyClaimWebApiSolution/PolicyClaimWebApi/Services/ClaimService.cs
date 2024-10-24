using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;

namespace PolicyClaimWebApi.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<int, Claim> _repository;
        private readonly IRepository<int, ClaimFile> _claimFileRepo;
        private readonly string _fileStoragePath;

        public ClaimService(IRepository<int, Claim> repository, IRepository<int, ClaimFile> claimFileRepo)
        {
            _repository = repository;
            _claimFileRepo = claimFileRepo;
            _fileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            if (!Directory.Exists(_fileStoragePath))
            {
                Directory.CreateDirectory(_fileStoragePath);
            }
        }
        public async Task<Claim> CreateClaim(CreateClaimDTO entity)
        {
            var claim = new Claim
            {
                PolicyNumber = entity.PolicyNumber,
                ClaimantId = entity.ClaimantId,
                ClaimDate = entity.ClaimDate
            };

            await _repository.Add(claim);

            if (entity.Files != null && entity.Files.Count > 0)
            {
                foreach (var file in entity.Files)
                {
                    if (file.Length > 0)
                    {
                        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                        var filePath = Path.Combine(_fileStoragePath, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var claimFile = new ClaimFile
                        {
                            ClaimID = claim.ClaimID, 
                            FilePath = filePath 
                        };

                        await _claimFileRepo.Add(claimFile);
                    }
                }
            }

            return claim;
        }

        public Task<IEnumerable<Claim>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
