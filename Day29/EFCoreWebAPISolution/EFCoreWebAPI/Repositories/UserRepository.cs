using EFCoreWebAPI.Context;
using EFCoreWebAPI.Interfaces;
using EFCoreWebAPI.Models;
using EFCoreWebAPI.Exceptions;

namespace EFCoreWebAPI.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ShoppingContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<User> Add(User entity)
        {
            try
            {
                _context.Users.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not add user");
                throw new CouldNotAddException("User");
            }
        }

        public Task<User> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(string key)
        {
            var user =  _context.Users.FirstOrDefault(u => u.Username == key);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = _context.Users.ToList();
            if (users.Any()) {
                return users;
            }
            throw new CollectionEmptyException("Users");
        }

        public Task<User> Update(User entity, int pid)
        {
            throw new NotImplementedException();
        }
    }
}
