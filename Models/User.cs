using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Models;

/// <summary>
/// Model used for authentication
/// </summary>
[Index(nameof(Username), IsUnique = true)]
public class User : Auditable
{
    [JsonIgnore] [Key] public Guid Id { get; set; }

    /// <summary>
    /// Your username
    /// </summary>
    /// <example>epic_user</example>
    [Required,
     RegularExpression(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,29}$", ErrorMessage = "Please use a valid username")]
    public string? Username { get; set; }

    /// <summary>
    /// Your password
    /// </summary>
    /// <example>VerySecure1</example>
    [Required,
     RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[a-zA-Z]).{3,}$",
         ErrorMessage = "Password must have a number and 3 characters minimum")]
    public string? Password { get; set; }

    [JsonIgnore] public ICollection<Address>? Addresses { get; set; }

    public Roles Role { get; set; } = Roles.Customer;

    /// <summary>
    /// Role of the user can be Admin(0) or Customer(1)
    /// </summary>
    public enum Roles
    {
        Admin,
        Customer
    }
}

[NotMapped]
public class LoginRequest
{
    /// <summary>
    /// Your username
    /// </summary>
    /// <example>epic_user</example>
    [Required,
     RegularExpression(@"^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,29}$", ErrorMessage = "Please use a valid username")]
    public string? Username { get; set; }

    /// <summary>
    /// Your password
    /// </summary>
    /// <example>VerySecure1</example>
    [Required,
     RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[a-zA-Z]).{3,}$",
         ErrorMessage = "Password must have a number and 3 characters minimum")]
    public string? Password { get; set; }
}