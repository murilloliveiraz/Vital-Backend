using Application.Helpers;

namespace Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest emailRequest);
    }
}
