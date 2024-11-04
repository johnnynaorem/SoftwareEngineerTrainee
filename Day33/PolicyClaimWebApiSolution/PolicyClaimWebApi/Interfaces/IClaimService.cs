using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Interfaces
{
    public interface IClaimService
    {
        Task<Claim> CreateClaim(CreateClaimDTO entity);
        Task<IEnumerable<Claim>> GetAll();
        Task<Claim> UpdateClaimStatus(UpdateClaimStatusDTO updateClaimStatus);

    }
}
