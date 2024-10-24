using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Models.DTOs;

namespace PolicyClaimWebApi.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<int, Policy> _repository;

        public PolicyService(IRepository<int, Policy> repository)
        {
            _repository = repository;
        }
        public async Task<string> CreatePolicy(PolicyDTO entity)
        {
            Policy policy = new Policy
            {
                AboutPolicy = entity.AboutPolicy,
                PolicyNumber = entity.PolicyNumber,
            };

            var addPolicy = await _repository.Add(policy);
            return addPolicy.PolicyNumber;
        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            var policies = await _repository.GetAll();
            return policies;
        }
    }
}
