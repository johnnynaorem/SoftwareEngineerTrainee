namespace PolicyClaimWebApi.Exceptions
{
    public class NotFound_CouldNotUpdateException:Exception
    {
        string msg;

        public NotFound_CouldNotUpdateException(string msg)
        {
            this.msg = msg;
        }

        public override string Message => msg;
    }
}
