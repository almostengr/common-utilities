using Almostengr.Common.Email.DomainServices;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Almostengr.Common.Email.Shared;

public static class EmailExtensions
{
    public static void AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISendEmailService, SendEmailService>();
        services.AddTransient<IReadEmailService, ReadImapEmailService>();

        services.AddTransient<ISmtpClient, SmtpClient>();
        services.AddTransient<IImapClient, ImapClient>();

        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
    }
}