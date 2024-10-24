using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Models.DTOs;
using System.Security.Claims;

namespace InsurancePolicyWebAPI.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<int, Policy> _repo;
        public PolicyService(IRepository<int, Policy>repository)
        {
            _repo = repository;
        }
        public async Task<string> CreatePolicy(PolicyDTO entity)
        {
            Policy newPo = new Policy
            {
                PolicyNumber = entity.PolicyNumber,
                PolicyDetail = entity.PolicyDetail
            };

            var addPolicy = await _repo.Add(newPo);
            return addPolicy.PolicyNumber;
        }
        public async Task<IEnumerable<Policy>> GetAll()
        {
            var policies = await _repo.GetAll();
            return policies;
        }
    }
}
