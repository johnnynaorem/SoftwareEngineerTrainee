using System.Runtime.Serialization;

namespace UnderstandingStructureApp.Exceptions
{
    internal class NoSuchImageException : Exception
    {
        string msg;
        public override string Message => msg;
        public NoSuchImageException()
        {
            msg = "No Such Image";
        }
    }
}