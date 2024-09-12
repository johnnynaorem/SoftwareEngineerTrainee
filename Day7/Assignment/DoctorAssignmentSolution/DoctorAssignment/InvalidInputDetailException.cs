

namespace DoctorAssignment
{
    
    internal class InvalidInputDetailException : Exception
    {
            string message;
            public InvalidInputDetailException()
            {
                message = "The details entered are invalid. Please try again.";
            }
        public override string Message => message;
    }
}