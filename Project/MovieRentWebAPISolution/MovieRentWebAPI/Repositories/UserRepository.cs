
using MovieRentWebAPI.Context;
using MovieRentWebAPI.Exceptions;
using MovieRentWebAPI.Interfaces;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Repositories
{
    public class UserRepository : IRepository<string, User>
    {
        private readonly MovieRentContext _context;

        public UserRepository(MovieRentContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<User> Add(User entity)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserEmail == entity.UserEmail);
                if (user == null)
                {
                    await _context.Users.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                else
                {
                    throw new InvalidOperationException("User already Exist.... Please LogIn");
                }
            }
            catch (Exception ex) {
                throw new CouldNotAddException($"User Add Failed....{ex.Message}");
            }
        }

        public async Task<User> Delete(string key)
        {
            var deletedUser = await Get(key);
            if (deletedUser != null) {
                _context.Users.Remove(deletedUser);
                await _context.SaveChangesAsync();
                return deletedUser;
            }
            throw new Exception("Delete Failed");
        }

        public async Task<User> Get(string key)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserEmail == key);
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = _context.Users.ToList();
            if (users.Any())
            {
                return users;
            }
            throw new EmptyCollectionException("Users Collection Empty");
        }

        public async Task<User> Update(User entity, string key)
        {
            var updatedUser = await Get(key);
            if (updatedUser != null) 
            {
                updatedUser.UserEmail = entity.UserEmail;
                updatedUser.UserName = entity.UserName;
                updatedUser.Password = entity.Password;

                await _context.SaveChangesAsync();
                return updatedUser;
            }
            throw new CouldNotUpdateException("Update Failed");
        }
    }
}
