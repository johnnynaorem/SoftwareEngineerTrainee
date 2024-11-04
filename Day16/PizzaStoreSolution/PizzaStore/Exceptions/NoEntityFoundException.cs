using PizzaStore.Models;
using System.Runtime.Serialization;

namespace PizzaStore.Exceptions
{
    public class NoEntityFoundException : Exception
    {
        string msg;
        public override string Message => msg;
        public NoEntityFoundException()
        {
            msg = "No Entity";
        }

        public NoEntityFoundException(string customer, int key)
        {
            msg = $"No {customer} with ID {key}";
        }

        public NoEntityFoundException(string customer)
        {
            msg = $"No {customer} Entity";
        }

    }
}