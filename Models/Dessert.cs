﻿using System.ComponentModel.DataAnnotations;

namespace kuro_desserts.Models;

/// <summary>
/// Represent a dessert for user to order
/// </summary>
public class Dessert
{
    [Key] public Guid Id { get; set; }

    /// <summary>
    /// Name of the dessert
    /// </summary>
    /// <example>Cake</example>
    [Required, MinLength(3, ErrorMessage = "Please use a name bigger than 3 characters"),
     MaxLength(100, ErrorMessage = "Please use a name less than 100 characters")]
    public string? Name { get; set; }

    /// <summary>
    /// Price of the dessert
    /// </summary>
    /// <example>10</example>
    [Required, Range(0, 100, ErrorMessage = "Please enter a valid and fair price")]
    public int Price { get; set; }

    /// <summary>
    /// Image of the dessert
    /// </summary>
    /// <example>https://kuro-dessserts.com/dessert.jpg</example>
    [Required, Url]
    public string? Image { get; set; }
}