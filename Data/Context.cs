using kuro_desserts.Models;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Data;

public class Context : DbContext
{
    public DbSet<Address>? Addresses { get; set; }

    public DbSet<Cart>? Carts { get; set; }

    public DbSet<Dessert>? Desserts { get; set; }

    public DbSet<Flavor>? Flavors { get; set; }

    public DbSet<Order>? Orders { get; set; }

    public DbSet<OrderTopping>? OrderToppings { get; set; }

    public DbSet<Topping>? Toppings { get; set; }

    public DbSet<User>? Users { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
}