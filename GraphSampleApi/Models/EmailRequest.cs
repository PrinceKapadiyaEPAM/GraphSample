namespace GraphSampleApi.Models;

public record EmailAttachment(string FileName, byte[] Content, string ContentType);

public class EmailRequest
{
    public List<string> To { get; set; } = [];
    public List<string> Cc { get; set; } = [];
    public List<string>? Bcc { get; set; }
    public string Subject { get; set; } = "";
    public string Body { get; set; } = "";
    public bool IsHtml { get; set; } = true;
    public List<EmailAttachment> Attachments { get; set; } = [];
}
