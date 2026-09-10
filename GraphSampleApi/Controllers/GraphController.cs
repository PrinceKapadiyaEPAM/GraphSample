using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web.Resource;

namespace GraphSampleApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GraphController : ControllerBase
{
    private static readonly string[] RequiredScopes = ["Read"];
    private readonly GraphServiceClient _graph;

    public GraphController(GraphServiceClient graph)
    {
        _graph = graph;
    }

    /// <summary>Gets the signed-in user's profile from Microsoft Graph (OBO flow).</summary>
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        HttpContext.VerifyUserHasAnyAcceptedScope(RequiredScopes);
        var user = await _graph.Me.GetAsync();
        return Ok(user);
    }
}
