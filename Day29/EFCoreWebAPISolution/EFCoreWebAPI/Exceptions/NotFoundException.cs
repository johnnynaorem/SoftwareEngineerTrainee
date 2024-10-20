using System.Runtime.Serialization;

namespace EFCoreWebAPI.Exceptions
{
    public class NotFoundException : Exception
    {

        string msg;

        public NotFoundException(string message)
        {
            this.msg = $"Not Found in {message}";
        }

        public override string Message => msg;
    }
}