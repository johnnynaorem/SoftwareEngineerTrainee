namespace PolicyClaimWebApi.Exceptions
{
    public class CouldNotAddException:Exception
    {
        string msg;

        public CouldNotAddException(string msg)
        {
            this.msg = msg;
        }

        public override string Message => msg;
    }
}
