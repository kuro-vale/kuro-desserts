using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace kuro_desserts.Models;

public class Cart
{
    [Key, JsonIgnore] public Guid Id { get; set; }

    [JsonIgnore] public ICollection<Order>? Orders { get; set; }

    [ForeignKey("UserId"), JsonIgnore] public Guid UserId { get; set; }

    [JsonIgnore] public User? User { get; set; }

    [ForeignKey("AddressId")] public Guid AddressId { get; set; }

    [JsonIgnore] public Address? Address { get; set; }

    public decimal GetTotalPrice()
    {
        return Orders?.Sum(order => order.GetTotalPrice()) ?? 0;
    }
}