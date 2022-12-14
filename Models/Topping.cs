using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Models;

/// <summary>
/// A topping for the dessert, WHAT ELSE DO YOU WANT?
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class Topping : Auditable
{
    [Key] public Guid Id { get; set; }

    /// <summary>
    /// Name of the topping
    /// </summary>
    /// <example>Gummy Bears</example>
    [Required, MinLength(3, ErrorMessage = "Please use a name bigger than 3 characters"),
     MaxLength(100, ErrorMessage = "Please use a name less than 100 characters")]
    public string? Name { get; set; }

    /// <summary>
    /// Price of the topping
    /// </summary>
    /// <example>5</example>
    [Required, Range(0, 50, ErrorMessage = "Please enter a valid and fair price (0..50)")]
    public int Price { get; set; }
}