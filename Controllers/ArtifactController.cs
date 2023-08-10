using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artifactan.Controllers;

[ApiController]
[Route("artifact")]
[Authorize]
public class ArtifactController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllArtifact()
    {
        var user = HttpContext.Items["User"];
        ;
        return Ok(user ?? "Success");
    }
}