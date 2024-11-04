using InsurancePolicyWebAPI.Models.DTOs;
using InsurancePolicyWebAPI.Models;

namespace InsurancePolicyWebAPI.Interfaces
{
    public interface IClaimService
    {
        Task<int> CreateClaim(ClaimDTO entity);
        Task<IEnumerable<Claim>> GetAll();
    }
}
