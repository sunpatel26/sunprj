using Business.Entities;

namespace Business.Interface
{
    public interface IEmailService
    {
        bool SendEmail(MailRequest mailRequest);
        void SendEmail(MailRequestMetadata request);
    }
}
