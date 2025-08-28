using Church.Domain.Models;

namespace Church.Infrastructure.Contracts.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, GenericMailMessage genericEmailModel);
    }
}
