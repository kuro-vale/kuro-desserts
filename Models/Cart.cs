using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kuro_desserts.Models;

public class Cart
{
    [Key] public Guid Id { get; set; }

    public ICollection<Order>? Orders { get; set; }

    [ForeignKey("UserId")] public Guid UserId { get; set; }

    public User? User { get; set; }

    [ForeignKey("AddressId")] public Guid AddressId { get; set; }

    public Address? Address { get; set; }

    public decimal GetTotalPrice()
    {
        return Orders?.Sum(order => order.GetTotalPrice()) ?? 0;
    }
}