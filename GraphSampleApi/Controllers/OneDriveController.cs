using GraphSampleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraphSampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OneDriveController : ControllerBase
{
    private readonly IOneDriveService _oneDrive;

    public OneDriveController(IOneDriveService oneDrive)
    {
        _oneDrive = oneDrive;
    }

    /// <summary>Uploads a file (max 4 MB) to the signed-in user's OneDrive.</summary>
    [HttpPost("upload")]
    [RequestSizeLimit(4 * 1024 * 1024)]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var webUrl = await _oneDrive.UploadSmallFileAsync(file.FileName, stream);
        return Ok(new { webUrl });
    }
}
