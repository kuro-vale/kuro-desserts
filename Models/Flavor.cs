using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Models;

/// <summary>
/// Change the dessert flavor, yummy
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class Flavor
{
    [Key] public Guid Id { get; set; }

    /// <summary>
    /// The name of the flavor
    /// </summary>
    /// <example>Salted Caramel</example>
    [Required, MinLength(3, ErrorMessage = "Please use a name bigger than 3 characters"),
     MaxLength(50, ErrorMessage = "Please use a name less than 50 characters")]
    public string? Name { get; set; }
}