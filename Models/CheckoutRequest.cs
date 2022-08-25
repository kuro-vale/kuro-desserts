using System.ComponentModel.DataAnnotations;

namespace kuro_desserts.Models;

public class CheckoutRequest
{
    /// <summary>
    /// The cart will be used to reference the orders to a user and an address
    /// </summary>
    [Required] public Cart Cart { get; set; } = null!;

    /// <summary>
    /// The list of orders made
    /// </summary>
    [Required] public IEnumerable<Order> Orders { get; set; } = null!;

    /// <summary>
    /// The toppings reference an order from the list of orders above
    /// </summary>
    public IEnumerable<OrderTopping>? OrderToppings { get; set; }
}