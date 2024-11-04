using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Models.DTOs;
using System.Security.Claims;
using Claim = InsurancePolicyWebAPI.Models.Claim;

namespace InsurancePolicyWebAPI.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IRepository<int, Claim> _repo;

        public ClaimService(IRepository<int, Claim> repository)
        {
            _repo = repository;
        }
        public async Task<int> CreateClaim(ClaimDTO entity)
        {
            Claim claim = new Claim
            {
                ClaimTypeId = entity.ClaimTypeId,
                PolicyNumber = entity.PolicyNumber,
                DateOfIncident = entity.DateOfIncident,
                ClaimantId = entity.ClaimantId,

            };
            var addClaim = await _repo.Add(claim);
            return addClaim.Id;
        }

        public async Task<IEnumerable<Models.Claim>> GetAll()
        {
            var claims = await _repo.GetAll();
            return claims;
        }
    }
}
