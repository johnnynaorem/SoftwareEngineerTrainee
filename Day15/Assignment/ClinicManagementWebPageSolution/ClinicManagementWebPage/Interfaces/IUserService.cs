using ClinicManagementWebPage.Models;

namespace ClinicManagementWebPage.Interfaces
{
    public interface IUserService
    {
        User GetUser(string email, string password);
        List<User> GetAllUser();
    }
}
