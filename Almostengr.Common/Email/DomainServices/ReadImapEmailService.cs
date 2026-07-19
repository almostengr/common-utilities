using Almostengr.Common.Common.DomainServices.Results;
using Almostengr.Common.Email.Shared;
using MailKit;
using MailKit.Net.Imap;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Almostengr.Common.Email.DomainServices;

public class ReadImapEmailService : IReadEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<ReadImapEmailService> _logger;
    private readonly IImapClient _client;

    public ReadImapEmailService(
        IOptions<EmailSettings> emailSettings,
        IImapClient imapClient,
        ILogger<ReadImapEmailService> logger
    )
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;

        _client = imapClient;
        _client.Connect(_emailSettings.Hostname, _emailSettings.PortNumber, _emailSettings.UseSsl);
        _client.Authenticate(_emailSettings.UserName, _emailSettings.Password);
    }

    public async Task<Result<List<MimeMessage>>> ReadAllAsync(string folderName = "INBOX")
    {
        try
        {
            Result<string> connectionResult = IsConnectedAndAuthorized();
            if (connectionResult.Failed)
            {
                return Result<List<MimeMessage>>.Failure(connectionResult.Errors);
            }

            var selectedFolder = SelectFolder(folderName);

            List<MimeMessage> messages = new();
            messages.AddRange(selectedFolder);

            return Result<List<MimeMessage>>.Success(messages);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<List<MimeMessage>>.Failure(ex.Message);
        }
    }

    public async Task<Result<string>> DeleteAsync(List<int> messageIds, string folderName = "INBOX")
    {
        try
        {
            Result<string> connectionResult = IsConnectedAndAuthorized();
            if (connectionResult.Failed)
            {
                return connectionResult;
            }

            var selectedFolder = SelectFolder(folderName);

            foreach (var messageId in messageIds)
            {
                await selectedFolder.StoreAsync(
                    messageId, new StoreFlagsRequest(StoreAction.Add, MessageFlags.Deleted));
            }

            await selectedFolder.ExpungeAsync();

            return Result<string>.Success(null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<string>.Failure(ex.Message);
        }
    }

    private Result<string> IsConnectedAndAuthorized()
    {
        if (!_client.IsConnected)
        {
            return Result<string>.Failure(EmailConstants.NotConnectedMessage);
        }

        if (!_client.IsAuthenticated)
        {
            return Result<string>.Failure(EmailConstants.NotAuthenticatedMessage);
        }

        return Result<string>.Success(null);
    }

    public async Task<Result<string>> MarkAsReadAsync(List<int> messageIds, string folderName = "INBOX")
    {
        try
        {
            Result<string> connectionResult = IsConnectedAndAuthorized();
            if (connectionResult.Failed)
            {
                return connectionResult;
            }

            var selectedFolder = SelectFolder(folderName);

            foreach (var messageId in messageIds)
            {
                await selectedFolder.StoreAsync(
                    messageId, new StoreFlagsRequest(StoreAction.Add, MessageFlags.Seen) { Silent = true });
            }

            return Result<string>.Success(null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result<string>.Failure(ex.Message);
        }
    }

    private IMailFolder SelectFolder(string folderName)
    {
        var selectedFolder = _client.GetFolder(folderName);
        selectedFolder.Open(FolderAccess.ReadWrite);
        return selectedFolder;
    }
}
