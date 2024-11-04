using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Repositories;

namespace PolicyClaimWebApi.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<int, Claimant> _claimRepo;
        private readonly IRepository<int, Claim> _repository;
        private readonly IRepository<int, ClaimFile> _claimFileRepo;
        private readonly string _fileStoragePath;

        public ClaimService(IRepository<int, Claim> repository, IRepository<int, ClaimFile> claimFileRepo, IRepository<int, Claimant> claimantRepository)
        {
            _claimRepo = claimantRepository;
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
            var claimant = new Claimant
            {
                Name = entity.ClaimantName,
                Phone = entity.ClaimantPhone,
                Email = entity.ClaimantEmail,
            };

            var addedClaimant = await _claimRepo.Add(claimant);

            var claim = new Claim
            {
                PolicyNumber = entity.PolicyNumber,
                ClaimantId = addedClaimant.ClaimantId,
                ClaimDate = entity.ClaimDate,
                TypeName = entity.ClaimType,
               
            };

            var addedClaim = await _repository.Add(claim);

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
                            ClaimID = addedClaim.ClaimID, 
                            FilePath = filePath 
                        };

                        await _claimFileRepo.Add(claimFile);
                    }
                }
            }

            return claim;
        }

        public async Task<IEnumerable<Claim>> GetAll()
        {
            var claims = await _repository.GetAll();
            return claims;
        }

        public async Task<Claim> UpdateClaimStatus(UpdateClaimStatusDTO updateClaimStatus)
        {
            var claim = new Claim
            {
                Status = updateClaimStatus.Status
            };
            var updateClaim = await _repository.Update(claim, updateClaimStatus.ClaimId);
            return updateClaim;
        }
    }
}
