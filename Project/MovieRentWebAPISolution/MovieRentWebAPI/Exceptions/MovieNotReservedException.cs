namespace MovieRentWebAPI.Exceptions
{
    public class MovieNotReservedException:Exception
    {
        string msg;
        public MovieNotReservedException(string message)
        {
            this.msg = message;
        }

        public override string Message => msg;
    }
}
