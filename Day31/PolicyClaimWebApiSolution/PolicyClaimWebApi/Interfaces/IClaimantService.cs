using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Interfaces
{
    public interface IClaimantService
    {
        Task<int> CreateClaiment(ClaimantDTO entity);
        Task<IEnumerable<Claimant>> GetAll();
    }
}
