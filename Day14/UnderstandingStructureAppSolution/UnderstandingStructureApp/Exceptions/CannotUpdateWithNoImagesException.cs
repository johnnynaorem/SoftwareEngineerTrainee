using System.Runtime.Serialization;

namespace UnderstandingStructureApp.Exceptions
{
    [Serializable]
    internal class CannotUpdateWithNoImagesException : Exception
    {
        string msg;
        public override string Message => msg;
        public CannotUpdateWithNoImagesException()
        {
            msg = "Cannot update with no Images";
        }

    }
}