using GraphSampleApi.Models;
using GraphSampleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphSampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MailController : ControllerBase
{
    private readonly IEmailService _email;

    public MailController(IEmailService email)
    {
        _email = email;
    }

    /// <summary>Sends an email via Microsoft Graph using the configured sender account.</summary>
    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        await _email.SendEmailAsync(request);
        return NoContent();
    }
}
