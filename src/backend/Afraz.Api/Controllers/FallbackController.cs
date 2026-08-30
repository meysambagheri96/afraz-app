using Microsoft.AspNetCore.Mvc;

namespace Afraz.Api.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public sealed class FallbackController(IWebHostEnvironment environment) : ApiControllerBase
{
    [Route("api/{**path}", Order = int.MaxValue)]
    [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")]
    public IActionResult ApiNotFound()
    {
        return Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "API endpoint not found.");
    }

    [HttpGet("{**path}", Order = int.MaxValue)]
    public IActionResult SpaFallback()
    {
        var indexFile = environment.WebRootFileProvider.GetFileInfo("index.html");

        if (!indexFile.Exists || indexFile.PhysicalPath is null)
        {
            return NotFound();
        }

        return PhysicalFile(indexFile.PhysicalPath, "text/html");
    }
}
