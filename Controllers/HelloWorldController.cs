using Microsoft.AspNetCore.Mvc;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class HelloWorldController : ControllerBase
{
    /// <summary>
    /// Example endpoint for documentation
    /// </summary>
    /// <param name="greet">Sample param description</param>
    /// <remarks>Sample remark</remarks>
    /// <response code="200">Sample description</response>
    /// <response code="500">Error</response>
    [HttpGet("{greet}")]
    public IActionResult Get(string greet)
    {
        return Ok($"Hello {greet}");
    }
}