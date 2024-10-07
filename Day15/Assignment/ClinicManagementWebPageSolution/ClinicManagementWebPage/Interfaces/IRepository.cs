using ClinicManagementWebPage.Models;

namespace ClinicManagementWebPage.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
        T Get(string email, string password);
        void Add(T entity); 
        void Update(T entity); 
        void Delete(int id);
    }
}
