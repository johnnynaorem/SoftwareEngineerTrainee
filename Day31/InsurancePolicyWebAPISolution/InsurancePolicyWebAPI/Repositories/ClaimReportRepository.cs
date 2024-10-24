using InsurancePolicyWebAPI.Context;
using InsurancePolicyWebAPI.Exceptions;
using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyWebAPI.Repositories
{
    public class ClaimReportRepository : IRepository<int, ClaimReport>
    {
        private readonly PolicyContext _context;

        public ClaimReportRepository(PolicyContext context)
        {
            _context = context;
        }
        public async Task<ClaimReport> Add(ClaimReport entity)
        {
            try
            {
                _context.ClaimReports.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("ClaimReport not add");
            }
        }

        public Task<ClaimReport> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimReport> Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClaimReport>> GetAll()
        {
            var claimReports = _context.ClaimReports.ToList();
            if (claimReports.Any())
            {
                return claimReports;
            }
            throw new EmptyCollectionException("ClaimReport Collection is empty");
        }

        public Task<ClaimReport> Update(ClaimReport entity)
        {
            throw new NotImplementedException();
        }
    }
}
