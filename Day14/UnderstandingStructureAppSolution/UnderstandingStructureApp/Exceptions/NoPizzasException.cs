using System.Runtime.Serialization;

namespace UnderstandingStructureApp.Exceptions
{
    internal class NoPizzasException : Exception
    {
        string msg;
        public NoPizzasException()
        {
            msg = "No Pizzas";
        }
        public override string Message => msg;
    }
}   