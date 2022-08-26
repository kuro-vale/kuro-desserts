using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace kuro_desserts.Models;

public class Order
{
    [Key, JsonIgnore] public Guid Id { get; set; }

    [ForeignKey("DessertId")] public Guid DessertId { get; set; }

    [JsonIgnore] public Dessert? Dessert { get; set; }

    [ForeignKey("FlavorId")] public Guid FlavorId { get; set; }

    [JsonIgnore] public Flavor? Flavor { get; set; }

    [ForeignKey("CartId"), JsonIgnore] public Guid CartId { get; set; }

    [JsonIgnore] public ICollection<OrderTopping>? Toppings { get; set; }

    /// <summary>
    /// Change Size of the dessert between 9 and 24 oz
    /// </summary>
    /// <example>24</example>
    [Required, Range(9, 24, ErrorMessage = "Dessert size must be between 9 and 24 oz")]
    public int Size { get; set; }

    [JsonIgnore, NotMapped] private const int DefaultSize = 12;

    public decimal GetToppingsPrice()
    {
        return Toppings?.Sum(topping => topping.Topping!.Price) ?? 0;
    }

    public decimal GetTotalPrice()
    {
        return GetToppingsPrice() + (decimal)Dessert!.Price * Size / DefaultSize;
    }
}

public class OrderResponse
{
    public Dessert? Dessert { get; set; }
    public Flavor? Flavor { get; set; }
    public ICollection<Topping>? Toppings { get; set; }

    public Address? Address { get; set; }
}