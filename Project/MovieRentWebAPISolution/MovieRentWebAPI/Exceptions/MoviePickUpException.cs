namespace MovieRentWebAPI.Exceptions
{
    public class MoviePickUpException:Exception
    {
        string msg;

        public MoviePickUpException(string message)
        {
            this.msg = message;
        }
        public override string Message => msg;
    }
}
