using kuro_desserts.Models;
using Microsoft.EntityFrameworkCore;

namespace kuro_desserts.Data;

public class Context : DbContext
{
    public DbSet<Address> Addresses { get; set; } = null!;

    public DbSet<Cart> Carts { get; set; } = null!;

    public DbSet<Dessert> Desserts { get; set; } = null!;

    public DbSet<Flavor> Flavors { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderTopping> OrderToppings { get; set; } = null!;

    public DbSet<Topping> Toppings { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Auditable && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((Auditable)entityEntry.Entity).UpdatedAt = DateTime.Now;

            if (entityEntry.State == EntityState.Added)
            {
                ((Auditable)entityEntry.Entity).CreatedAt = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }
}