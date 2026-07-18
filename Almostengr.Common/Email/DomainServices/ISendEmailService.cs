using Almostengr.Common.Common.DomainServices.Results;

namespace Almostengr.Common.Email.DomainServices;

public interface ISendEmailService
{
    Task<Result<string>> ExecuteAsync(List<string> toEmails, string subject, string body);
    Task<Result<string>> ExecuteAsync(List<string> toEmails, string subject, string body, List<string> ccEmail);
    Task<Result<string>> ExecuteAsync(List<string> toEmails, string subject, string body, List<string> ccEmail, List<string> bccEmail);
}