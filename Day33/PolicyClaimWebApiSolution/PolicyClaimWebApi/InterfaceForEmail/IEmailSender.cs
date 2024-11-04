using PolicyClaimWebApi.Email;

namespace PolicyClaimWebApi.InterfaceForEmail
{
    public interface IEmailSender
    {
        void SendEmail(Message email);
    }
}
