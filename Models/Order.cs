using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kuro_desserts.Models;

public class Order
{
    [Key] public Guid Id { get; set; }

    [ForeignKey("DessertId")] public Guid DessertId { get; set; }

    public Dessert? Dessert { get; set; }

    [ForeignKey("FlavorId")] public Guid FlavorId { get; set; }


    [ForeignKey("CartId")] public Guid CartId { get; set; }

    public ICollection<OrderTopping>? Toppings { get; set; }

    /// <summary>
    /// Change Size of the dessert between 9 and 24 oz
    /// </summary>
    /// <example>24</example>
    [Required, Range(9, 24, ErrorMessage = "Dessert size must be between 9 and 24 oz")]
    public int Size { get; set; }

    public int DefaultSize { get; set; } = 12;

    public decimal GetToppingsPrice()
    {
        return Toppings?.Sum(topping => topping.Topping!.Price) ?? 0;
    }

    public decimal GetTotalPrice()
    {
        return GetToppingsPrice() + (decimal)Dessert!.Price * Size / DefaultSize;
    }
}