using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Exceptions;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Repositories
{
    public class ClaimFileRepository:IRepository<int, ClaimFile>
    {
        private readonly PolicyContext _context;

        public ClaimFileRepository(PolicyContext policyContext)
        {
            _context = policyContext;
        }

        public async Task<ClaimFile> Add(ClaimFile entity)
        {
            try
            {
                _context.ClaimFiles.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Fail to add ClaimFile");
            }
        }

        public Task<ClaimFile> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimFile> Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClaimFile>> GetAll()
        {
            var claimFiles = _context.ClaimFiles.ToList();
            if (claimFiles.Any())
            {
                return claimFiles;
            }
            throw new EmptyCollectionException("claimFiles collection is empty");
        }

        public Task<ClaimFile> Update(ClaimFile entity, int key)
        {
            throw new NotImplementedException();
        }
    }
}
