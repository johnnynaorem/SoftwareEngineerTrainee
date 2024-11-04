using MovieRentWebAPI.EmailModels;

namespace MovieRentWebAPI.EmailInterface
{
    public interface IEmailSender
    {
        void SendEmail(Message email);
    }
}
