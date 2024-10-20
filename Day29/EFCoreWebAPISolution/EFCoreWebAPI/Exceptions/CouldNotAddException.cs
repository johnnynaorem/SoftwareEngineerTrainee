using System.Runtime.Serialization;

namespace EFCoreWebAPI.Exceptions
{
    public class CouldNotAddException : Exception
    {
        string msg;

        public CouldNotAddException(string message)
        {
            this.msg =$"Not Add into {message}";
        }
        public override string Message => msg;
    }
}