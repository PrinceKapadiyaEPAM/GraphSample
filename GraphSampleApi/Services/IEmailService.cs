using GraphSampleApi.Models;

namespace GraphSampleApi.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequest request);
}
