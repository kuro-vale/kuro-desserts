using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Models;

/// <summary>
/// Model used for authentication
/// </summary>
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key] public Guid Id { get; set; }

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
    /// <example>10</example>
    [Required,
     RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[a-zA-Z]).{3,}$",
         ErrorMessage = "Password must have a number and 3 characters minimum")]
    public string? Password { get; set; }

    public ICollection<Address>? Addresses { get; set; }
}