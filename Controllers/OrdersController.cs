using kuro_desserts.Data;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class OrdersController : ControllerBase
{
    private readonly Context _context;

    public OrdersController(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all orders made by logged user
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="204">No Content</response>
    [HttpGet]
    public IActionResult Index()
    {
        var user = (User?)HttpContext.Items["User"];
        if (user == null) return Unauthorized("Please login to do this");

        var carts = _context.Carts.Where(cart => cart.UserId == user.Id)
            .Include(cart => cart.Address)
            .Include(cart => cart.Orders)!.ThenInclude(order => order.Dessert)
            .Include(cart => cart.Orders)!.ThenInclude(order => order.Flavor)
            .Include(cart => cart.Orders)!.ThenInclude(order => order.Toppings)!
            .ThenInclude(topping => topping.Topping);

        var payload = new List<OrderResponse>();

        foreach (var cart in carts)
        {
            payload.AddRange(from order in cart.Orders!
                let toppings = order.Toppings!.Select(topping => topping.Topping!).ToList()
                select new OrderResponse
                    { Dessert = order.Dessert, Flavor = order.Flavor, Toppings = toppings, Address = cart.Address });
        }

        if (!payload.Any()) return NoContent();
        return Ok(payload);
    }
}