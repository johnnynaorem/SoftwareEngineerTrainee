using System.Runtime.Serialization;

namespace ClinicManagementWebPage.Exceptions
{
    internal class InvalidUserException : Exception
    {
        string msg;
        public InvalidUserException()
        {
            msg = "Invalid email and password, Try Again";
        }
        public override string Message => msg;
    }
}