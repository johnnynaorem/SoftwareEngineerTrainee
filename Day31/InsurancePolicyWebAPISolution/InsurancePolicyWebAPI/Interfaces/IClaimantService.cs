using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Models.DTOs;

namespace InsurancePolicyWebAPI.Interfaces
{
    public interface IClaimantService
    {
        Task<int> CreateClaiment(ClaimantDTO entity);
        Task<IEnumerable<Claimant>> GetAll();
    }
}
