using ClinicManagementWebPage.Exceptions;
using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;

namespace ClinicManagementWebPage.Repositories
{
    public class UserRepository : IRepository<User>
    {
        static List<User> users = new List<User>()
        {
            new User(){Id = 1, Name = "Johnny Naorem", Email = "johnny@gmail.com", Image = "https://cmkt-image-prd.freetls.fastly.net/0.1.0/ps/5397226/1820/2730/m1/fpnw/wm1/ndvld9i93olzarioqzuu6a3bbnxody4gfmpvjyorvip7x9alegcm2kti4tc3ktsv-.jpg?1542627643&s=cfeb8b5f8850e9a99b7761a2c34e27f8", Password="johnny@02"},
            new User(){Id = 2, Name = "Rohit Laishram", Email = "rohit@gmail.com", Image = "https://cmkt-image-prd.freetls.fastly.net/0.1.0/ps/5397226/1820/2730/m1/fpnw/wm1/ndvld9i93olzarioqzuu6a3bbnxody4gfmpvjyorvip7x9alegcm2kti4tc3ktsv-.jpg?1542627643&s=cfeb8b5f8850e9a99b7761a2c34e27f8", Password="rohit@12"}
        };
        public List<User> GetAll()
        {
            return users;
        }

        public User Get(string email, string password) {
            var user = users.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user == null)
            {
                throw new InvalidUserException();
            }
            return user;
        }
        public User Get(int uid)
        {
            var user = users.FirstOrDefault(x => x.Id == uid);
            if( user == null)
            {
                throw new InvalidCastException();
            }
            return user;
        }


        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
