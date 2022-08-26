using kuro_desserts.Data;
using kuro_desserts.Models;
using Microsoft.AspNetCore.Mvc;

namespace kuro_desserts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CheckoutController : ControllerBase
{
    private readonly Context _context;

    public CheckoutController(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Save orders to database
    /// </summary>
    /// <param name="request">In orders you find products to add to cart,
    /// the cart references a user and an address to the orders</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    [HttpPost]
    public IActionResult Checkout([FromBody] CheckoutRequest request)
    {
        // Get authenticated user
        var user = (User?)HttpContext.Items["User"];
        if (user == null) return Unauthorized("Please login to do this");

        // Track cart if address belong to logged user 
        var cart = request.Cart;
        var userAddress = _context.Addresses.Find(cart.AddressId);

        if (userAddress == null) return NotFound($"Couldn't find address with id {cart.AddressId}");
        if (userAddress.UserId != user.Id) return StatusCode(403);

        cart.UserId = user.Id;
        _context.Carts.Add(cart);

        // Track orders if ids exist
        foreach (var order in request.Orders)
        {
            if (_context.Desserts.Find(order.DessertId) == null)
                return NotFound($"Couldn't find dessert with id {order.DessertId}");
            if (_context.Flavors.Find(order.FlavorId) == null)
                return NotFound($"Couldn't find flavor with id {order.FlavorId}");

            order.CartId = request.Cart.Id;
        }

        _context.Orders.AddRange(request.Orders);

        // Track Order Toppings if provided and if topping ids exist
        if (request.OrderToppings != null)
        {
            foreach (var orderTopping in request.OrderToppings)
            {
                if (_context.Toppings.Find(orderTopping.ToppingId) == null)
                    return NotFound($"Couldn't find topping with id {orderTopping.ToppingId}");

                try
                {
                    orderTopping.OrderId = request.Orders.ToList()[orderTopping.OrderListIndex].Id;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return BadRequest(
                        "OrderListIndex was out of range. Must be non-negative and less than the size of the order list.");
                }
            }

            _context.OrderToppings.AddRange(request.OrderToppings);
        }

        // Save changes if not error found
        _context.SaveChanges();
        return Ok("Successfully checkout");
    }
}