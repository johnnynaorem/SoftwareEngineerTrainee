using System.Runtime.Serialization;

namespace PizzaStore.Exceptions
{

    internal class CollectionEmptyException : Exception
    {
        string msg;
        public override string Message => msg;
        public CollectionEmptyException()
        {
            msg = "No Collections";
        }

        public CollectionEmptyException(string customer)
        {
            msg = $"No {customer}";
        }
    }
}