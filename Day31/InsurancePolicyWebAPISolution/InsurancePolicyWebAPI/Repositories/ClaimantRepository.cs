using InsurancePolicyWebAPI.Context;
using InsurancePolicyWebAPI.Exceptions;
using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Interfaces;

namespace InsurancePolicyWebAPI.Repositories
{
    public class ClaimantRepository : IRepository<int, Claimant>
    {
        private readonly PolicyContext _context;

        public ClaimantRepository(PolicyContext context)
        {
            _context = context;
        }
        public async Task<Claimant> Add(Claimant entity)
        {
            try
            {
                _context.Claimants.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception) {
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
            if (claimants.Any()) 
            {
                return claimants;
            }
            throw new EmptyCollectionException();
        }

        public Task<Claimant> Update(Claimant entity)
        {
            throw new NotImplementedException();
        }
    }
}
