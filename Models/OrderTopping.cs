using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace kuro_desserts.Models;

public class OrderTopping
{
    [Key, JsonIgnore] public Guid Id { get; set; }

    [ForeignKey("OrderId"), JsonIgnore] public Guid OrderId { get; set; }

    [ForeignKey("ToppingId")] public Guid ToppingId { get; set; }

    [JsonIgnore] public Topping? Topping { get; set; }

    /// <summary>
    /// The index of the order in the list of orders in your checkout request, starting from 0
    /// </summary>
    [NotMapped, Required] public int OrderListIndex { get; set; }
}