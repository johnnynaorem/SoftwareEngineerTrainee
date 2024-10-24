using InsurancePolicyWebAPI.Models.DTOs;
using InsurancePolicyWebAPI.Models;

namespace InsurancePolicyWebAPI.Interfaces
{
    public interface IClaimTypeService
    {
        Task<int> CreateClaiment(ClaimTypeDTO entity);
        Task<IEnumerable<ClaimType>> GetAll();
    }
}
