using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Exceptions;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly PolicyContext _context;

        public UserRepository(PolicyContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User entity)
        {
            try
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Fail to add Claimant");
            }
        }

        public Task<User> Delete(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(string key)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == key);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users =  _context.Users.ToList();
            if (users.Any())
            {
                return users;
            }
            throw new EmptyCollectionException("User collection is empty");
        }

        public Task<User> Update(User entity, string key)
        {
            throw new NotImplementedException();
        }
    }
}
