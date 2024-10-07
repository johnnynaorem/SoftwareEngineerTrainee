using LoginWebPage.Interface;
using LoginWebPage.Models;

namespace LoginWebPage.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> repository)//comming as an injection from the provider
        {
            _userRepository = repository;
        }

        public User GetUser(int uid, string password)
        {
            User existUser = _userRepository.GetUser(uid, password);
            return existUser;
        }

        public User GetUser(int id)
        { 
            return _userRepository.GetUser(id); 
        }
    }
}
