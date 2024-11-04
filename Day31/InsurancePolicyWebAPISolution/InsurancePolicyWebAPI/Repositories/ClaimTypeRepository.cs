using InsurancePolicyWebAPI.Context;
using InsurancePolicyWebAPI.Exceptions;
using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyWebAPI.Repositories
{
    public class ClaimTypeRepository : IRepository<int, ClaimType>
    {
        private readonly PolicyContext _context;

        public ClaimTypeRepository(PolicyContext context)
        {
            _context = context;
        }
        public async Task<ClaimType> Add(ClaimType entity)
        {
            try
            {
                _context.ClaimTypes.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Fail to add ClaimType");
            }
        }

        public Task<ClaimType> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimType> Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClaimType>> GetAll()
        {
            var claimTypes = _context.ClaimTypes.ToList();
            if (claimTypes.Any())
            {
                return claimTypes;
            }
            throw new EmptyCollectionException("ClaimType collection is empty");
        }

        public Task<ClaimType> Update(ClaimType entity)
        {
            throw new NotImplementedException();
        }
    }
}
