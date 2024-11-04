namespace MovieRentWebAPI.Exceptions
{
    public class CouldNotUpdateException:Exception
    {
        string message;
        public CouldNotUpdateException(string msg)
        {
            message = msg;
        }
        public override string Message => message;
    }
}
