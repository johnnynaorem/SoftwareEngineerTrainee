using System.Runtime.Serialization;

namespace LoginWebPage.Repositories
{
    public class InvalidUserCredentials : Exception
    {
        public InvalidUserCredentials()
            : base("Invalid username or password.")
        {
        }

        public InvalidUserCredentials(string message)   
            : base(message)
        {
        }

        public InvalidUserCredentials(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}