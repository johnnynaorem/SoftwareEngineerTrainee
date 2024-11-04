using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;

namespace ClinicManagementWebPage.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userService) {
            _userRepository = userService;
        }
        public User GetUser(string email, string password)
        {
            User user = _userRepository.Get(email, password);
            return user;
        }

        public List<User> GetAllUser()
        {
            var user = _userRepository.GetAll();
            return user;
        }
    }
}
