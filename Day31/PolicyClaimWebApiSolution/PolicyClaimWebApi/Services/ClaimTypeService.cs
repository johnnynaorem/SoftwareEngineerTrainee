using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;

namespace PolicyClaimWebApi.Services
{
    public class ClaimTypeService : IClaimTypeService
    {
        private readonly IRepository<int, ClaimType> _repository;

        public ClaimTypeService(IRepository<int, ClaimType> repository)
        {
            _repository = repository;
        }
        public async Task<string> CreateClaimType(ClaimTypeDTO entity)
        {
            ClaimType claimType = new ClaimType
            {
                TypeDescription = entity.TypeDescription,
                TypeName = entity.TypeName,
                PolicyNumber = entity.PolicyNumber,
            };

            var addClaimType = await _repository.Add(claimType);
            return addClaimType.TypeName;
        }

        public async Task<IEnumerable<ClaimType>> GetAll()
        {
            var claimTypes = await _repository.GetAll();
            return claimTypes;
        }
    }
}
