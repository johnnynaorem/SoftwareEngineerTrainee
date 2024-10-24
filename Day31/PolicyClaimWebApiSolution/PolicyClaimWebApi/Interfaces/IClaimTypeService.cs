using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Interfaces
{
    public interface IClaimTypeService
    {
        Task<int> CreateClaimType(ClaimTypeDTO entity);
        Task<IEnumerable<ClaimType>> GetAll();
    }
}
