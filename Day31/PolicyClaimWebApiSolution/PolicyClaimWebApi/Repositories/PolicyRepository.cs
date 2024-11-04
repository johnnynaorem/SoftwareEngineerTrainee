using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Exceptions;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Repositories
{
    public class PolicyRepository : IRepository<int, Policy>
    {
        private readonly PolicyContext _context;

        public PolicyRepository(PolicyContext context)
        {
            _context = context;
        }
        public async Task<Policy> Add(Policy entity)
        {
            try
            {
                _context.Policies.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Fail to add Policy");
            }
        }

        public Task<Policy> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<Policy> Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            var policies =  _context.Policies.ToList();
            if (policies.Any()) 
            {
                return policies;
            }
            throw new EmptyCollectionException("Policies Collection Empty");
        }


        public Task<Policy> Update(Policy entity, int key)
        {
            throw new NotImplementedException();
        }
    }
}
