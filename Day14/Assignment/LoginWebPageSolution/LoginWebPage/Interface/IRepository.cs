namespace LoginWebPage.Interface
{
    public interface IRepository<T> where T : class
    {
        T GetUser(int uid, string password);
        T GetUser(int uid);
    }
}
