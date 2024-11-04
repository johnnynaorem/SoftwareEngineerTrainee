using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Exceptions;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Repositories
{
    public class ClaimantRepository : IRepository<int, Claimant>
    {
        private readonly PolicyContext _context;

        public ClaimantRepository(PolicyContext policyContext)
        {
            _context = policyContext;
        }
        public async Task<Claimant> Add(Claimant entity)
        {
            try
            {
                _context.Claimants.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Fail to add Claimant");
            }
        }

        public Task<Claimant> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<Claimant> Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Claimant>> GetAll()
        {
            var claimants = _context.Claimants.ToList();
            if(claimants.Any())
            {
                return claimants;
            }
            throw new EmptyCollectionException("Claimants Collection Empty");
        }

        public Task<Claimant> Update(Claimant entity, int key)
        {
            throw new NotImplementedException();
        }
    }
}
