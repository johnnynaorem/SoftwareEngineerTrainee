using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Interfaces
{
    public interface IClaimFileService
    {
        Task<int> CreateClaimFile(ClaimFile entity);
        Task<IEnumerable<Claimant>> GetAll();
    }
}
