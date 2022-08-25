using kuro_desserts.Data;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FlavorsController : ControllerBase
{
    private readonly Context _context;

    public FlavorsController(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all flavors
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="204">No Content</response>
    [HttpGet]
    public IActionResult Index()
    {
        var payload = _context.Flavors.Where(flavor => flavor.IsDeleted == false);

        if (!payload.Any()) return NoContent();

        return Ok(payload);
    }

    /// <summary>
    /// Show a flavor by id
    /// </summary>
    /// <param name="id">The id of the flavor to show</param>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id:guid}")]
    public IActionResult Show(Guid id)
    {
        var payload = _context.Flavors.Find(id);

        if (payload == null || payload.IsDeleted) return NotFound($"Couldn't find flavor with id {id}");

        return Ok(payload);
    }

    /// <summary>
    /// Create a new flavor
    /// </summary>
    /// <param name="flavor">An ID will be automatically generated, you don't need to enter one</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpPost]
    public IActionResult Store([FromBody] Flavor flavor)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        flavor.Id = new Guid();
        _context.Flavors.Add(flavor);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return BadRequest($"A flavor with the name '{flavor.Name}' already exists");
        }

        return Ok(flavor);
    }

    /// <summary>
    /// Update a flavor
    /// </summary>
    /// <param name="id">The id of the flavor to update</param>
    /// <param name="flavorRequest">Update the name of the flavor</param>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] Flavor flavorRequest)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        var flavor = _context.Flavors.Find(id);

        if (flavor == null || flavor.IsDeleted) return NotFound($"Couldn't find flavor with id {id}");

        flavor.Name = flavorRequest.Name;

        _context.Flavors.Update(flavor);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return BadRequest($"A flavor with the name '{flavor.Name}' already exists");
        }

        return Ok(flavor);
    }

    /// <summary>
    /// Soft delete a flavor
    /// </summary>
    /// <param name="id">Id of the flavor to delete</param>
    /// <response code="204">No Content</response>
    /// <response code="404">Not Found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        var flavor = _context.Flavors.Find(id);

        if (flavor == null || flavor.IsDeleted) return NotFound($"Couldn't find flavor with id {id}");

        flavor.IsDeleted = true;

        _context.Flavors.Update(flavor);
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