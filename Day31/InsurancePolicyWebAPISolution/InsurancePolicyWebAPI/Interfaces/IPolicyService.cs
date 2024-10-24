using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Models.DTOs;

namespace InsurancePolicyWebAPI.Interfaces
{
    public interface IPolicyService
    {
        Task<string> CreatePolicy(PolicyDTO entity);
        Task<IEnumerable<Policy>> GetAll();
    }
}
