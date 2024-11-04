

namespace Day7Task
{
    internal class InvalidInputDetailException : Exception
    {
        string message;
        public InvalidInputDetailException()
        {
            message = "The details entered are invalid. Please try again.";
        }
        //public override string Message
        //{
        //    get
        //    {
        //        return message;
        //    }
        //}

        public override string Message => message;//Lambda Expression or arrow function
    }
}