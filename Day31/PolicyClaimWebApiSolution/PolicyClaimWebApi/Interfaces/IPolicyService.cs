using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Interfaces
{
    public interface IPolicyService
    {
        Task<string> CreatePolicy(PolicyDTO entity);
        Task<IEnumerable<Policy>> GetAll();
    }
}
