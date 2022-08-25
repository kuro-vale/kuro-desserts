using kuro_desserts.Data;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ToppingsController : ControllerBase
{
    private readonly Context _context;

    public ToppingsController(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all toppings
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="204">No Content</response>
    [HttpGet]
    public IActionResult Index()
    {
        var payload = _context.Toppings.Where(topping => topping.IsDeleted == false);

        if (!payload.Any()) return NoContent();

        return Ok(payload);
    }

    /// <summary>
    /// Show a topping by id
    /// </summary>
    /// <param name="id">The id of the topping to show</param>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id:guid}")]
    public IActionResult Show(Guid id)
    {
        var payload = _context.Toppings.Find(id);

        if (payload == null || payload.IsDeleted) return NotFound($"Couldn't find topping with id {id}");

        return Ok(payload);
    }

    /// <summary>
    /// Create a new topping
    /// </summary>
    /// <param name="topping">An ID will be automatically generated, you don't need to enter one</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpPost]
    public IActionResult Store([FromBody] Topping topping)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        topping.Id = new Guid();
        _context.Toppings.Add(topping);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return BadRequest($"A topping with the name '{topping.Name}' already exists");
        }

        return Ok(topping);
    }

    /// <summary>
    /// Update a topping
    /// </summary>
    /// <param name="id">The id of the topping to update</param>
    /// <param name="toppingRequest">Update the name of the topping</param>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] Topping toppingRequest)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        var topping = _context.Toppings.Find(id);

        if (topping == null || topping.IsDeleted) return NotFound($"Couldn't find topping with id {id}");

        topping.Name = toppingRequest.Name;
        topping.Price = toppingRequest.Price;

        _context.Toppings.Update(topping);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return BadRequest($"A topping with the name '{topping.Name}' already exists");
        }

        return Ok(topping);
    }

    /// <summary>
    /// Soft delete a topping
    /// </summary>
    /// <param name="id">Id of the topping to delete</param>
    /// <response code="204">No Content</response>
    /// <response code="404">Not Found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (IsAdmin(out var statusCode)) return statusCode;

        var topping = _context.Toppings.Find(id);

        if (topping == null || topping.IsDeleted) return NotFound($"Couldn't find topping with id {id}");

        topping.IsDeleted = true;

        _context.Toppings.Update(topping);
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