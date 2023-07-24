using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artifactan.Controllers;

[Authorize]
[ApiController]
[Route("artifact")]
public class ArtifactController : ControllerBase
{

    [HttpGet("/")]
    public async Task<IActionResult> GetAllArtifact()
    {
        return Ok("Success");
    }

}