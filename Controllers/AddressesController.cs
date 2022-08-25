using kuro_desserts.Data;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Mvc;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AddressesController : ControllerBase
{
    private readonly Context _context;

    public AddressesController(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all user's addresses
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="204">No Content</response>
    [HttpGet]
    public IActionResult Index()
    {
        var user = (User?)HttpContext.Items["User"];

        if (user == null) return Unauthorized("Please login to do this");

        var payload = _context.Addresses.Where(address => address.UserId == user.Id);

        if (!payload.Any()) return NoContent();

        return Ok(payload);
    }

    /// <summary>
    /// Show a address by id
    /// </summary>
    /// <param name="id">The id of the address to show</param>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id:guid}")]
    public IActionResult Show(Guid id)
    {
        return HasPermissions(id, out var address, out var statusCode) ? statusCode : Ok(address);
    }

    /// <summary>
    /// Create a new address
    /// </summary>
    /// <param name="address">An ID will be automatically generated, you don't need to enter one</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    public IActionResult Store([FromBody] Address address)
    {
        var user = (User?)HttpContext.Items["User"];

        if (user == null) return Unauthorized("Please login to do this");

        address.Id = new Guid();
        address.UserId = user.Id;
        _context.Addresses.Add(address);
        _context.SaveChanges();

        return Ok(address);
    }

    /// <summary>
    /// Update an address
    /// </summary>
    /// <param name="id">The id of the address to update</param>
    /// <param name="addressRequest"></param>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] Address addressRequest)
    {
        if (HasPermissions(id, out var address, out var statusCode)) return statusCode;

        address!.Line = addressRequest.Line;
        address.City = addressRequest.City;
        address.PostalCode = addressRequest.PostalCode ?? address.PostalCode;

        _context.Addresses.Update(address);
        _context.SaveChanges();

        return Ok(address);
    }

    /// <summary>
    /// Delete an address
    /// </summary>
    /// <param name="id">Id of the address to delete</param>
    /// <response code="204">No Content</response>
    /// <response code="404">Not Found</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        if (HasPermissions(id, out var address, out var statusCode)) return statusCode;

        _context.Addresses.Remove(address!);
        _context.SaveChanges();

        return NoContent();
    }

    private bool HasPermissions(Guid id, out Address? address, out IActionResult statusCode)
    {
        var user = (User?)HttpContext.Items["User"];

        if (user == null)
        {
            statusCode = Unauthorized("Please login to do this");
            address = null;
            return true;
        }

        address = _context.Addresses.Find(id);

        if (address == null)
        {
            statusCode = NotFound($"Couldn't find address with id {id}");
            return true;
        }

        if (address.UserId != user.Id)
        {
            statusCode = StatusCode(403);
            return true;
        }

        statusCode = null!;
        return false;
    }
}