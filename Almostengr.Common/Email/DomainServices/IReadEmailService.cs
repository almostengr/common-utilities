using Almostengr.Common.Common.DomainServices.Results;
using MimeKit;

namespace Almostengr.Common.Email.DomainServices;

public interface IReadEmailService
{
    Task<Result<List<MimeMessage>>> ReadAllAsync(string folderName = "INBOX");
    Task<Result<string>> DeleteAsync(List<int> messageIds, string folderName = "INBOX");
    Task<Result<string>> MarkAsReadAsync(List<int> messageIds, string folderName = "INBOX");
}