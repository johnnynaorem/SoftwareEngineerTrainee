using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Exceptions;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Repositories
{
    public class ClaimRepository : IRepository<int, Claim>
    {
        private readonly PolicyContext _context;

        public ClaimRepository(PolicyContext policyContext)
        {
            _context = policyContext;
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

        public async Task<Claim> Get(int key)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == key);
            return claim;
        }

        public async Task<IEnumerable<Claim>> GetAll()
        {
            var claims = _context.Claims.ToList();
            if (claims.Any())
            {
                return claims;
            }
            throw new EmptyCollectionException("Claims Collection Empty");
        }

        public async Task<Claim> Update(Claim entity, int key)
        {
            var claim = await Get(key);
            if (claim != null)
            {
                claim.Status = entity.Status;
                await _context.SaveChangesAsync();
                return claim;
            }
            throw new NotFound_CouldNotUpdateException("Claim Not Found, Cannot Update status");
        }
    }
}
