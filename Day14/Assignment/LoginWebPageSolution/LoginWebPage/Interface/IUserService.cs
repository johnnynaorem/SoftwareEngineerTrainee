using LoginWebPage.Models;

namespace LoginWebPage.Interface
{
    public interface IUserService
    {
        User GetUser(int uid, string password);
        User GetUser(int uid);
    }
}
