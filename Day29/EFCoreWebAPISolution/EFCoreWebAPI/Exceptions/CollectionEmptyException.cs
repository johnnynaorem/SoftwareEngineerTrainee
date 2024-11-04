using System.Runtime.Serialization;

namespace EFCoreWebAPI.Exceptions
{
    [Serializable]
    public class CollectionEmptyException : Exception
    {
        string msg;

        public CollectionEmptyException(string message) {
            this.msg = $"Empty Collection - {message}";
        }

        public override string Message => msg;
    }

}