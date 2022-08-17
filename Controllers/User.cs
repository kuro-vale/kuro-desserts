using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Controllers;

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
    [Required]
    public int Price { get; set; }
}