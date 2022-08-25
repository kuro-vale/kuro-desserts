using kuro_desserts.Data;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DessertsController : ControllerBase
{
    private readonly Context _context;

    public DessertsController(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all desserts
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="204">No Content</response>
    [HttpGet]
    public IActionResult Index()
    {
        var payload = _context.Desserts.Where(dessert => dessert.IsDeleted == false);

        if (!payload.Any()) return NoContent();

        return Ok(payload);
    }

    /// <summary>
    /// Show a dessert by id
    /// </summary>
    /// <param name="id">The id of the dessert to show</param>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id:guid}")]
    public IActionResult Show(Guid id)
    {
        var payload = _context.Desserts.Find(id);

        if (payload == null || payload.IsDeleted) return NotFound($"Couldn't find dessert with id {id}");

        return Ok(payload);
    }

    /// <summary>
    /// Create a new dessert
    /// </summary>
    /// <param name="dessert">An ID will be automatically generated, you don't need to enter one</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpPost]
    public IActionResult Store([FromBody] Dessert dessert)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        dessert.Id = new Guid();
        _context.Desserts.Add(dessert);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return BadRequest($"A dessert with the name '{dessert.Name}' already exists");
        }

        return Ok(dessert);
    }

    /// <summary>
    /// Update a dessert
    /// </summary>
    /// <param name="id">The id of the dessert to update</param>
    /// <param name="dessertRequest">Only name, price and image will be processed</param>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] Dessert dessertRequest)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        var dessert = _context.Desserts.Find(id);

        if (dessert == null || dessert.IsDeleted) return NotFound($"Couldn't find dessert with id {id}");

        dessert.Name = dessertRequest.Name;
        dessert.Image = dessertRequest.Image;
        dessert.Price = dessertRequest.Price;

        _context.Desserts.Update(dessert);
        _context.SaveChanges();

        return Ok(dessert);
    }

    /// <summary>
    /// Soft delete a dessert
    /// </summary>
    /// <param name="id">Id of the dessert to delete</param>
    /// <response code="204">No Content</response>
    /// <response code="404">Not Found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        var dessert = _context.Desserts.Find(id);

        if (dessert == null || dessert.IsDeleted) return NotFound($"Couldn't find dessert with id {id}");

        dessert.IsDeleted = true;

        _context.Desserts.Update(dessert);
        _context.SaveChanges();

        return NoContent();
    }

    private bool IsAdmin(out IActionResult statusCode)
    {
        var user = (User?)HttpContext.Items["User"];

        if (user == null)
        {
            statusCode = Unauthorized("Please login to do this");
            return true;
        }

        if (user.Role == Models.User.Roles.Customer)
        {
            statusCode = StatusCode(403, "You don't have the permissions to do this");
            return true;
        }

        statusCode = null!;
        return false;
    }
}