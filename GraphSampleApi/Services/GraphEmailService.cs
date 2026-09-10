using GraphSampleApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;

namespace GraphSampleApi.Services;

public class GraphEmailService(
    [FromKeyedServices("app-graph")] GraphServiceClient graphClient,
    IConfiguration configuration) : IEmailService
{
    public async Task SendEmailAsync(EmailRequest request)
    {
        string senderEmail = configuration["Email:SenderEmail"]!;
        var message = new Message
        {
            Subject = request.Subject,
            Body = new ItemBody { ContentType = request.IsHtml ? BodyType.Html : BodyType.Text, Content = request.Body },
            ToRecipients = [.. request.To.Select(ToRecipient)],
            CcRecipients = request.Cc.Count > 0 ? [.. request.Cc.Select(ToRecipient)] : [],
            BccRecipients = request.Bcc?.Count > 0 ? [.. request.Bcc.Select(ToRecipient)] : [],
        };
        if (request.Attachments.Count > 0)
            message.Attachments = [.. request.Attachments.Select(a => (Attachment)new FileAttachment
            {
                Name = a.FileName,
                ContentBytes = a.Content,
                ContentType = a.ContentType
            })];
        await graphClient.Users[senderEmail].SendMail.PostAsync(
            new SendMailPostRequestBody { Message = message, SaveToSentItems = true });
    }

    private static Recipient ToRecipient(string email) =>
        new() { EmailAddress = new EmailAddress { Address = email } };
}
