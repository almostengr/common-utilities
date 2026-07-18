using Almostengr.Common.Common.DomainServices.Results;

namespace Almostengr.Common.Email.DomainServices;

public interface IReadEmailService
{
    Task<Result<string>> ReadAllAsync(string folderName = "INBOX");
    Task<Result<string>> DeleteAsync(List<int> messageIds, string folderName = "INBOX");
    Task<Result<string>> MarkAsReadAsync(List<int> messageIds, string folderName = "INBOX");
}