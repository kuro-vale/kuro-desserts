using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kuro_desserts.Models;

public class OrderTopping
{
    [Key, ForeignKey("OrderId")] public Guid OrderId { get; set; }

    [ForeignKey("ToppingId")] public Guid ToppingId { get; set; }

    public Topping? Topping { get; set; }
}