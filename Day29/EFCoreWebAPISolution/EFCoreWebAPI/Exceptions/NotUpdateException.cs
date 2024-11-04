using System.Runtime.Serialization;

namespace EFCoreWebAPI.Exceptions
{
    [Serializable]
    public class NotUpdateException : Exception
    {
        string msg;
        public NotUpdateException(string message)
        {
            msg = message;
        }

        public override string Message => msg;

    }
}