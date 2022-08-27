using kuro_desserts.Models;

namespace kuro_desserts.Data;

public static class SeedData
{
    public static void Initialize(Context db)
    {
        var desserts = new List<Dessert>
        {
            new()
            {
                Name = "Sweet Bread",
                Price = 36,
                Image = "https://cdn.pixabay.com/photo/2015/05/04/10/03/honey-752145_960_720.jpg"
            },
            new()
            {
                Name = "Cupcake",
                Price = 28,
                Image = "https://cdn.pixabay.com/photo/2017/05/04/21/23/cupcakes-2285209_960_720.jpg"
            },
            new()
            {
                Name = "Ice Cream",
                Price = 11,
                Image = "https://cdn.pixabay.com/photo/2019/11/07/13/05/waffle-4608843_960_720.jpg"
            },
            new()
            {
                Name = "Cookie",
                Price = 12,
                Image = "https://cdn.pixabay.com/photo/2017/07/28/14/28/macarons-2548818_960_720.jpg"
            },
            new()
            {
                Name = "Parfait",
                Price = 37,
                Image = "https://cdn.pixabay.com/photo/2020/01/06/01/11/raspberry-4744355_960_720.jpg"
            },
            new()
            {
                Name = "Pie",
                Price = 43,
                Image = "https://cdn.pixabay.com/photo/2020/08/11/13/58/apple-pie-5479993_960_720.jpg"
            },
            new()
            {
                Name = "Cake",
                Price = 50,
                Image = "https://cdn.pixabay.com/photo/2017/01/11/11/33/cake-1971552_960_720.jpg"
            },
            new()
            {
                Name = "Doughnut",
                Price = 33,
                Image = "https://cdn.pixabay.com/photo/2019/09/14/08/08/donut-4475455_960_720.jpg"
            },
            new()
            {
                Name = "Pudding",
                Price = 15,
                Image = "https://cdn.pixabay.com/photo/2017/01/06/17/18/caramel-1958358_960_720.jpg"
            },
            new()
            {
                Name = "Cake Pop",
                Price = 38,
                Image = "https://cdn.pixabay.com/photo/2015/03/26/23/09/cake-pops-693645_960_720.jpg"
            },
            new()
            {
                Name = "Brownie",
                Price = 20,
                Image = "https://cdn.pixabay.com/photo/2014/11/28/08/03/brownie-548591_960_720.jpg"
            },
            new()
            {
                Name = "Trifle",
                Price = 30,
                Image = "https://cdn.pixabay.com/photo/2019/11/23/20/04/coffee-4648041_960_720.jpg"
            }
        };
        db.Desserts?.AddRange(desserts);

        var flavors = new List<Flavor>
        {
            new()
            {
                Name = "Strawberry"
            },
            new()
            {
                Name = "Butter Pecan"
            },
            new()
            {
                Name = "Red Velvet"
            },
            new()
            {
                Name = "Caramel"
            },
            new()
            {
                Name = "Vanilla"
            },
            new()
            {
                Name = "Apple"
            },
            new()
            {
                Name = "Salted Caramel"
            },
            new()
            {
                Name = "Butterscotch"
            },
            new()
            {
                Name = "Cherry"
            },
            new()
            {
                Name = "Chocolate"
            }
        };
        db.Flavors?.AddRange(flavors);

        var toppings = new List<Topping>
        {
            new()
            {
                Name = "Caramel",
                Price = 6
            },
            new()
            {
                Name = "Granola",
                Price = 2
            },
            new()
            {
                Name = "Chocolate Chips",
                Price = 5
            },
            new()
            {
                Name = "Frosting",
                Price = 7
            },
            new()
            {
                Name = "Glaze",
                Price = 8
            },
            new()
            {
                Name = "Toffee Bits",
                Price = 7
            },
            new()
            {
                Name = "Rainbow Sprinkles",
                Price = 3
            },
            new()
            {
                Name = "Chocolate Syrup",
                Price = 9
            },
            new()
            {
                Name = "Butterscotch Syrup",
                Price = 1
            },
            new()
            {
                Name = "Powdered Sugar",
                Price = 9
            }
        };
        db.Toppings?.AddRange(toppings);

        db.SaveChanges();
    }
}