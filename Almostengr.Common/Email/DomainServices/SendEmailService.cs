using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.Common.Email.Shared;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Almostengr.Common.Email.DomainServices;

public class SendEmailService : ISendEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ISmtpClient _client;

    public SendEmailService(
        IOptions<EmailSettings> emailSettings,
        ISmtpClient smtpClient
    )
    {
        _emailSettings = emailSettings.Value;

        _client = smtpClient;
        _client.Connect(_emailSettings.Hostname, _emailSettings.PortNumber, _emailSettings.UseSsl);
        _client.Authenticate(_emailSettings.UserName, _emailSettings.Password);
    }

    public async Task<Result<string>> ExecuteAsync(List<string> toEmails, string subject, string body)
    {
        return await ExecuteAsync(toEmails, subject, body, [], []);
    }

    public async Task<Result<string>> ExecuteAsync(
        List<string> toEmails, string subject, string body, List<string> ccEmail)
    {
        return await ExecuteAsync(toEmails, subject, body, ccEmail, []);
    }

    public async Task<Result<string>> ExecuteAsync(
        List<string> toEmails, string subject, string body, List<string> ccEmail, List<string> bccEmail)
    {
        try
        {
            var message = new MimeMessage();

            message.To.AddRange(toEmails.Select(e => new MailboxAddress(null, e)));

            message.To.AddRange(ccEmail.Select(e => new MailboxAddress(null, e)));

            message.To.AddRange(bccEmail.Select(e => new MailboxAddress(null, e)));

            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            _client.Send(message);
            _client.Disconnect(true);

            return Result<string>.Success(null);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(ex.Message);
        }
    }
}
