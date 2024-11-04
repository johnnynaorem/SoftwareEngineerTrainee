using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Email;
using PolicyClaimWebApi.InterfaceForEmail;
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
        private readonly IEmailSender _emailSender;
        private readonly string _fileStoragePath;

        public ClaimService(IRepository<int, Claim> repository, IRepository<int, ClaimFile> claimFileRepo, IRepository<int, Claimant> claimantRepository, IEmailSender emailSender)
        {
            _claimRepo = claimantRepository;
            _repository = repository;
            _claimFileRepo = claimFileRepo;
            _emailSender = emailSender;
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
            var updateClaim = await _repository.Update(claim, updateClaimStatus.ClaimID);
            if (updateClaim != null && updateClaim.Status == Status.Approved) {
                var rng = new Random();
                var message = new Message(new string[] { "johnnynaorem7@gmail.com" }, "Claim Status Updated", "Hey, Your Claim with ClaimId: " + updateClaim.ClaimID + "\nPolicy Number: " + updateClaim.PolicyNumber + "\nClaim Type: " + updateClaim.TypeName + " is Updated" + "\nStatus: " + updateClaim.Status);
                _emailSender.SendEmail(message);
            }
            return updateClaim;
        }
    }
}
