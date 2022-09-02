using kuro_desserts.Models;

namespace kuro_desserts.Services;

public class CartService
{
    public bool DessertModalShowed { get; set; }
    public Order? Order { get; set; }

    public Cart Cart { get; } = new()
    {
        Id = Guid.NewGuid(),
        Orders = new List<Order>()
    };

    public void ShowDessertModal(Dessert dessert)
    {
        Order = new Order
        {
            CartId = Cart.Id,
            DessertId = dessert.Id,
            Dessert = dessert,
            Toppings = new List<OrderTopping>(),
            Size = Order.DefaultSize
        };
        DessertModalShowed = true;
    }

    public void HideDessertModal()
    {
        Order = null;
        DessertModalShowed = false;
    }

    public void RemoveOrder(Order order)
    {
        Cart.Orders!.Remove(order);
    }
}
public class MessageService
{
    public event Action? OnMessage;
    public void SendMessage()
    {
        OnMessage?.Invoke();
    }
}