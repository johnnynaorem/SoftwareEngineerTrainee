using System.Runtime.Serialization;

namespace ClinicManagementWebPage.Exceptions
{
    public class InvalidAppointment : Exception
    {
        string msg;
        public InvalidAppointment()
        {
        }

        public InvalidAppointment(string message)
        {
            msg = message;
        }

        public override string Message => msg;
    }
}