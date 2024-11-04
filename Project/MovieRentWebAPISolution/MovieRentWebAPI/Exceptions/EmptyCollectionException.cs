namespace MovieRentWebAPI.Exceptions
{
    public class EmptyCollectionException:Exception
    {
        string msg;

        public EmptyCollectionException(string msg)
        {
            this.msg = msg;
        }

        public override string Message => msg;
    }
}
