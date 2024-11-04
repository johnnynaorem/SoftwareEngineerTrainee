using LoginWebPage.Interface;
using LoginWebPage.Models;
using System.Security.Authentication;

namespace LoginWebPage.Repositories
{
    public class Repository : IRepository<User>
    {
        List<User> users = new List<User>()
        {
            new User(){Id = 1, Name = "Johnny Naorem", Email = "johnny@gmail.com", Image = "https://cmkt-image-prd.freetls.fastly.net/0.1.0/ps/5397226/1820/2730/m1/fpnw/wm1/ndvld9i93olzarioqzuu6a3bbnxody4gfmpvjyorvip7x9alegcm2kti4tc3ktsv-.jpg?1542627643&s=cfeb8b5f8850e9a99b7761a2c34e27f8", Password="johnny@02"},
            new User(){Id = 2, Name = "Rohit Laishram", Email = "rohit@gmail.com", Image = "https://cmkt-image-prd.freetls.fastly.net/0.1.0/ps/5397226/1820/2730/m1/fpnw/wm1/ndvld9i93olzarioqzuu6a3bbnxody4gfmpvjyorvip7x9alegcm2kti4tc3ktsv-.jpg?1542627643&s=cfeb8b5f8850e9a99b7761a2c34e27f8", Password="rohit@12"}
        };
        public User GetUser(int uid, string password)
        {
            var isValidUser = users.FirstOrDefault(users => users.Id == uid && users.Password == password);
            if (isValidUser == null) {
                throw new InvalidUserCredentials();
            }
            return isValidUser;
        }

        public User GetUser(int uid)
        {
            var isValidUser = users.FirstOrDefault(users => users.Id == uid);
            if (isValidUser == null)
            {
                throw new InvalidUserCredentials();
            }
            return isValidUser;
        }
    }
}
