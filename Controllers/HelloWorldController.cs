using Microsoft.AspNetCore.Mvc;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloWorldController : ControllerBase
{
    [HttpGet(Name = "GetHelloWorld")]
    public IActionResult Get()
    {
        return Ok("Hello World");
    }
}