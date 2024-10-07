using System.Runtime.Serialization;

namespace UnderstandingStructureApp.Exceptions
{
    [Serializable]
    internal class CannotAddWithNoImagesException : Exception
    {
        string msg;
        public override string Message => msg;
        public CannotAddWithNoImagesException()
        {
            msg = "You Cannot add with no Images";
        }
    }
}