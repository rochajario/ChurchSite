using Church.Domain.Configurations;
using Church.Domain.Models;
using Church.Infrastructure.Contracts.Services;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Church.Infrastructure.Services
{
    public class EmailService(IOptions<EmailOptions> options) : IEmailService
    {
        private SmtpSender GetSender()
        {
            var smtpSettings = options.Value;
            return new SmtpSender(() => new SmtpClient(smtpSettings.SmtpHost)
            {
                EnableSsl = smtpSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = smtpSettings.SmtpPort,
                Credentials =
                    string.IsNullOrWhiteSpace(smtpSettings.Password)
                    ? null
                    : new NetworkCredential(smtpSettings.Username, smtpSettings.Password)
            });
        }

        public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
        {
            await new Email(new RazorRenderer(), GetSender())
                .SetFrom(options.Value.Username, options.Value.DisplayName)
                .To(to)
                .Subject(subject)
                .Body(body, isHtml)
                .SendAsync();
        }

        public async Task SendEmailAsync(string to, string subject, GenericMailMessage genericEmailModel)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            var templatePath = Path.Combine(assemblyPath, "Templates", "GenericEmail.cshtml");

            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"O arquivo de template não foi encontrado: {templatePath}");
            }

            await new Email(new RazorRenderer(), GetSender())
                .SetFrom(options.Value.Username, options.Value.DisplayName)
                .To(to)
                .Subject(subject)
                .UsingTemplateFromFile(templatePath, genericEmailModel, isHtml: true)
                .SendAsync();
        }
    }
}
