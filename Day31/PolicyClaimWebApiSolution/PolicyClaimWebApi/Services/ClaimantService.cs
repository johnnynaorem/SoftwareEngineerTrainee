using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;
using PolicyClaimWebApi.Repositories;

namespace PolicyClaimWebApi.Services
{
    public class ClaimantService : IClaimantService
    {
        private readonly IRepository<int, Claimant> _repository;

        public ClaimantService(IRepository<int,Claimant> repository)
        {
            _repository = repository;
        }
        public async Task<int> CreateClaiment(ClaimantDTO entity)
        {
            Claimant claimant = new Claimant
            {
                Name = entity.Name,
                Phone = entity.Phone,
                Email = entity.Email,
            };

            var addClaimant = await _repository.Add(claimant);
            return addClaimant.ClaimantId;
        }

        public async Task<IEnumerable<Claimant>> GetAll()
        {
            var claimants = await _repository.GetAll();
            return claimants;
        }
    }
}
