using System.ComponentModel.DataAnnotations;

namespace kuro_desserts.Models;

/// <summary>
/// User's address
/// </summary>
public class Address
{
    [Key] public Guid Id { get; set; }

    /// <summary>
    /// Address line
    /// </summary>
    /// <example>8288 Antoinette Pike</example>
    [Required, MinLength(5, ErrorMessage = "Please use an Address bigger than 5 letters."),
     MaxLength(100, ErrorMessage = "Please use an Address less than 100 letters.")]
    public string? Line { get; set; }

    /// <summary>
    /// City to send the dessert
    /// </summary>
    /// <example>Wunschtown</example>
    [Required, MinLength(3, ErrorMessage = "Please use a City bigger than 3 letters."),
     MaxLength(50, ErrorMessage = "Please use a City less than 50 letters.")]
    public string? City { get; set; }

    /// <summary>
    /// Your postal code
    /// </summary>
    /// <example>12345</example>
    [RegularExpression(@"^([0-9]{5})$", ErrorMessage = "Please use a valid Postal Code with five numbers.")]
    public string? PostalCode { get; set; }
}