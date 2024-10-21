using System.Runtime.Serialization;

namespace EFCoreWebAPI.Exceptions
{
    public class NotDeleteException : Exception
    {
        string msg;
        public NotDeleteException(string message)
        {
            msg = message; 
        }
        public override string Message => msg;
    }
}