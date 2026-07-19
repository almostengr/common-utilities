using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.Common.Email.Shared;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Almostengr.Common.Email.DomainServices;

public class SendEmailService : ISendEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<SendEmailService> _logger;
    private readonly ISmtpClient _client;

    public SendEmailService(
        IOptions<EmailSettings> emailSettings,
        ILogger<SendEmailService> logger,
        ISmtpClient smtpClient
    )
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;

        _client = smtpClient;
        _client.Connect(_emailSettings.Hostname, _emailSettings.PortNumber, _emailSettings.UseSsl);
        _client.Authenticate(_emailSettings.UserName, _emailSettings.Password);
    }

    public async Task<Result<string>> ExecuteAsync(List<string> toEmails, string subject, string body)
    {
        return await ExecuteAsync(toEmails, subject, body, [], []);
    }

    public async Task<Result<string>> ExecuteAsync(
        List<string> toEmails, string subject, string body, List<string> ccEmails)
    {
        return await ExecuteAsync(toEmails, subject, body, ccEmails, []);
    }

    public async Task<Result<string>> ExecuteAsync(
        List<string> toEmails, string subject, string body, List<string> ccEmails, List<string> bccEmails)
    {
        try
        {
            var message = new MimeMessage();

            message.To.AddRange(toEmails.Select(e => new MailboxAddress(null, e)));

            message.To.AddRange(ccEmails.Select(e => new MailboxAddress(null, e)));

            message.To.AddRange(bccEmails.Select(e => new MailboxAddress(null, e)));

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
            _logger.LogError(ex, ex.Message);
            return Result<string>.Failure(ex.Message);
        }
    }
}
