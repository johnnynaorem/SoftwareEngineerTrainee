using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Models.DTOs;

namespace InsurancePolicyWebAPI.Services
{
    public class ClaimTypeService : IClaimTypeService
    {
        private readonly IRepository<int, ClaimType> _repository;

        public ClaimTypeService(IRepository<int, ClaimType> repository)
        {
        
            _repository = repository;
        }
        public async Task<int> CreateClaiment(ClaimTypeDTO entity)
        {
            ClaimType claimType = new ClaimType
            {
                Name = entity.Name
            };

            var addClaimant = await _repository.Add(claimType);
            return addClaimant.Id;
        }

        public async Task<IEnumerable<ClaimType>> GetAll()
        {
            var claimants = await _repository.GetAll();
            return claimants;
        }
    }
}
