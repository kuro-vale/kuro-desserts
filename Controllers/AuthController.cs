using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using kuro_desserts.Data;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly Context _context;

    public AuthController(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="user">New user to create, username must be unique</param>
    /// <returns>JWT token</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        user.Id = Guid.NewGuid();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.Users.Add(user);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return BadRequest($"A user with the username {user.Username} already exists");
        }

        var token = GenerateToken(user);

        return Ok(token);
    }

    /// <summary>
    /// Create a new JWT token
    /// </summary>
    /// <param name="loginRequest">Verify user credentials</param>
    /// <returns>JWT token</returns>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == loginRequest.Username);

        if (user == null || user.IsDeleted) return Unauthorized();
        if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password)) return Unauthorized();

        var token = GenerateToken(user);
        return Ok(token);
    }

    /// <summary>
    /// Get logged user
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unatuhorized</response>
    [HttpGet("current")]
    public IActionResult GetCurrentUser()
    {
        var user = (User?)HttpContext.Items["User"];

        if (user == null)
        {
            return Unauthorized("Please login to do this");
        }

        user.Password = "HIDDEN";
        return Ok(user);
    }

    /// <summary>
    /// Update your user
    /// </summary>
    /// <param name="userRequest">Update your username and password</param>
    /// <response code="401">Unauthorized</response>
    [HttpPut]
    public IActionResult UpdateUser([FromBody] User userRequest)
    {
        var user = (User?)HttpContext.Items["User"];

        if (user == null)
        {
            return Unauthorized("Please login to do this");
        }

        user.Username = userRequest.Username;

        _context.Users.Update(user);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            return BadRequest($"A user with the username {user.Username} already exists");
        }

        return Ok("Success");
    }

    /// <summary>
    /// Delete your user
    /// </summary>
    /// <response code="401">Unauthorized</response>
    [HttpDelete]
    public IActionResult DeleteUser()
    {
        var user = (User?)HttpContext.Items["User"];

        if (user == null)
        {
            return Unauthorized("Please login to do this");
        }

        user.IsDeleted = true;

        _context.Users.Update(user);
        _context.SaveChanges();

        return NoContent();
    }


    private static string GenerateToken(User user)
    {
        var now = DateTime.UtcNow;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Iat, now.ToString(CultureInfo.InvariantCulture))
        };

        var token = new JwtSecurityToken("kuro-desserts", "", claims, expires: now.AddDays(15),
            signingCredentials: signIn);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}