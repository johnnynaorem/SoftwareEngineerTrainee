using System.Runtime.Serialization;

namespace PolicyClaimWebApi.Exceptions
{
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException()
        {
        }

        public EmptyCollectionException(string? message) : base(message)
        {
        }

        public EmptyCollectionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyCollectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}