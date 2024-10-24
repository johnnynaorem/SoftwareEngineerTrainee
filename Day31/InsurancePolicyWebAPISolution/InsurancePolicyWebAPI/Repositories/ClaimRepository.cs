using InsurancePolicyWebAPI.Context;
using InsurancePolicyWebAPI.Exceptions;
using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models;

namespace InsurancePolicyWebAPI.Repositories
{
    public class ClaimRepository:IRepository<int, Claim>
    {
        private readonly PolicyContext _context;

        public ClaimRepository(PolicyContext context)
        {
            _context = context;
        }

        public async Task<Claim> Add(Claim entity)
        {
            try
            {
                _context.Claims.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Fail to add Claim");
            }
        }

        public Task<Claim> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<Claim> Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Claim>> GetAll()
        {
            var claims =  _context.Claims.ToList();
            if (claims.Any())
            {
                return claims;
            }
            throw new EmptyCollectionException("Empty Collection Claim");
        }

        public Task<Claim> Update(Claim entity)
        {
            throw new NotImplementedException();
        }
    }
}