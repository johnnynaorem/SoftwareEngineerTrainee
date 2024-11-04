using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Interfaces
{
    public interface IClaimTypeService
    {
        Task<string> CreateClaimType(ClaimTypeDTO entity);
        Task<IEnumerable<ClaimType>> GetAll();
    }
}
