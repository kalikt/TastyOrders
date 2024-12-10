using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TastyOrders.Data.Models;

namespace TastyOrders.Data.Configuration
{
    using static TastyOrders.Common.EntityValidationConstants.MenuItem;
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder
                .Property(m => m.ImageUrl)
                .HasMaxLength(ImageUrlMaxLength);

            builder
                .Property(t => t.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder
                .HasOne(mi => mi.Restaurant)
                .WithMany(r => r.MenuItems)
                .HasForeignKey(mi => mi.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasData(this.SeedMenuItems());

        }

        private List<MenuItem> SeedMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>()
    {
        new MenuItem()
        {
            Id = 1,
            Name = "Veggie Burger",
            Description = "Delicious plant-based burger",
            Price = 12.99m,
            ImageUrl = "/images/veggieBurger.jpg",
            RestaurantId = 1
        },
        new MenuItem()
        {
            Id = 2,
            Name = "Beef Burger",
            Description = "Delicious burger with fresh meat",
            Price = 18.99m,
            ImageUrl = "/images/beefBurger.jpg",
            RestaurantId = 1
        },
        new MenuItem()
        {
            Id = 3,
            Name = "Chicken Burger",
            Description = "Delicious burger with chicken and cheese",
            Price = 15.99m,
            ImageUrl = "/images/chickenBurger.jpg",
            RestaurantId = 1
        },
        new MenuItem()
        {
            Id = 4,
            Name = "Spaghetti Bolognese",
            Description = "Classic Italian pasta with rich sauce",
            Price = 16.50m,
            ImageUrl = "/images/meatPasta.jpg",
            RestaurantId = 2
        },
        new MenuItem()
        {
            Id = 5,
            Name = "Margherita Pizza",
            Description = "Classic pizza with tomato, basil, and mozzarella",
            Price = 11.99m,
            ImageUrl = "/images/pizzaM.jpg",
            RestaurantId = 2
        },
        new MenuItem()
        {
            Id = 6,
            Name = "Caesar Salad",
            Description = "Fresh lettuce with Caesar dressing",
            Price = 10.99m,
            ImageUrl = "/images/ceaserSalad.jpg",
            RestaurantId = 2
        },
        new MenuItem()
        {
            Id = 7,
            Name = "Cheesecake",
            Description = "Our best seller",
            Price = 8.99m,
            ImageUrl = "/images/cheesecake.jpg",
            RestaurantId = 3
        },
        new MenuItem()
        {
            Id = 8,
            Name = "Chocolate cake",
            Description = "The richest chocolate taste you can find",
            Price = 7.99m,
            ImageUrl = "/images/chokoCake.jpg",
            RestaurantId = 3
        },
        new MenuItem()
        {
            Id = 9,
            Name = "Cream Brulee",
            Description = "Tastiest cream brulee you can find",
            Price = 5.99m,
            ImageUrl = "/images/creamB.jpg",
            RestaurantId = 3
        },
        new MenuItem()
        {
            Id = 10,
            Name = "Tomato Basil Soup",
            Description = "A creamy and flavorful tomato soup with fresh basil.",
            Price = 5.49m,
            ImageUrl = "/images/tomatoBasilSoup.jpg",
            RestaurantId = 4
        },
        new MenuItem()
        {
            Id = 11,
            Name = "Cobb Salad",
            Description = "Loaded salad with chicken, bacon, avocado, eggs, and blue cheese.",
            Price = 9.99m,
            ImageUrl = "/images/cobbSalad.jpg",
            RestaurantId = 4
        },
        new MenuItem()
        {
            Id = 12,
            Name = "French Onion Soup",
            Description = "Rich onion soup topped with a layer of melted cheese and croutons.",
            Price = 7.99m,
            ImageUrl = "/images/frenchOnionSoup.jpg",
            RestaurantId = 4
        },
        new MenuItem()
        {
            Id = 13,
            Name = "Chicken Tacos",
            Description = "Soft tacos filled with seasoned chicken and fresh toppings.",
            Price = 9.99m,
            ImageUrl = "/images/chickenTacos.jpg",
            RestaurantId = 5
        },
        new MenuItem()
        {
            Id = 14,
            Name = "Fish Tacos",
            Description = "Crispy battered fish served with cabbage slaw and a zesty lime crema.",
            Price = 11.49m,
            ImageUrl = "/images/fishTacos.jpg",
            RestaurantId = 5
        },
        new MenuItem()
        {
            Id = 15,
            Name = "Beef Barbacoa Tacos",
            Description = "Slow-cooked beef barbacoa with fresh cilantro, onions, and lime.",
            Price = 8.99m,
            ImageUrl = "/images/beefBarbacoaTacos.jpg",
            RestaurantId = 5
        },
        new MenuItem()
        {
            Id = 16,
            Name = "Grilled Salmon",
            Description = "Perfectly grilled salmon served with a side of vegetables.",
            Price = 15.99m,
            ImageUrl = "/images/grilledSalmon.jpg",
            RestaurantId = 6
        },
        new MenuItem()
        {
            Id = 17,
            Name = "Shrimp Scampi",
            Description = "Juicy shrimp cooked in a garlic butter sauce, served over linguine.",
            Price = 19.99m,
            ImageUrl = "/images/shrimpScampi.jpg",
            RestaurantId = 6
        },
        new MenuItem()
        {
            Id = 18,
            Name = "Steak Frites",
            Description = "Grilled steak served with a side of crispy French fries.",
            Price = 18.99m,
            ImageUrl = "/images/steakFrites.jpg",
            RestaurantId = 6
        },
        new MenuItem()
        {
            Id = 19,
            Name = "Chocolate Brownie",
            Description = "Rich chocolate brownie topped with vanilla ice cream.",
            Price = 5.99m,
            ImageUrl = "/images/chocolateBrownie.jpg",
            RestaurantId = 7
        },
        new MenuItem()
        {
            Id = 20,
            Name = "Classic Pancakes",
            Description = "Stack of fluffy pancakes served with syrup and butter.",
            Price = 5.49m,
            ImageUrl = "/images/classicPancakes.jpg",
            RestaurantId = 7
        },
        new MenuItem()
        {
            Id = 21,
            Name = "Chocolate Lava Cake",
            Description = "Warm chocolate cake with a gooey molten center, served with ice cream.",
            Price = 6.99m,
            ImageUrl = "/images/chocolateLavaCake.jpg",
            RestaurantId = 7
        },
        new MenuItem()
        {
            Id = 22,
            Name = "Fettuccine Alfredo",
            Description = "Creamy Alfredo sauce over perfectly cooked fettuccine.",
            Price = 12.99m,
            ImageUrl = "/images/fettuccineAlfredo.jpg",
            RestaurantId = 8
        },
        new MenuItem()
        {
            Id = 23,
            Name = "Spaghetti Carbonara",
            Description = "Classic Italian pasta with creamy egg sauce, pancetta, and Parmesan cheese.",
            Price = 12.99m,
            ImageUrl = "/images/spaghettiCarbonara.jpg",
            RestaurantId = 8
        },
        new MenuItem()
        {
            Id = 24,
            Name = "Penne Arrabbiata",
            Description = "Spicy tomato-based pasta dish with penne, garlic, and red chili flakes.",
            Price = 11.49m,
            ImageUrl = "/images/penneArrabbiata.jpg",
            RestaurantId = 8
        },
        new MenuItem()
        {
            Id = 25,
            Name = "Pepperoni Pizza",
            Description = "Classic pizza topped with pepperoni and melted mozzarella cheese.",
            Price = 13.99m,
            ImageUrl = "/images/pepperoniPizza.jpg",
            RestaurantId = 9
        },
        new MenuItem()
        {
            Id = 26,
            Name = "Four Cheese Pizza",
            Description = "A blend of mozzarella, cheddar, Parmesan, and blue cheese on a crispy crust.",
            Price = 11.99m,
            ImageUrl = "/images/fourCheesePizza.jpg",
            RestaurantId = 9
        },
        new MenuItem()
        {
            Id = 27,
            Name = "BBQ Chicken Pizza",
            Description = "Tender chicken, BBQ sauce, red onions, and cilantro on a wood-fired crust.",
            Price = 13.49m,
            ImageUrl = "/images/bbqChickenPizza.jpg",
            RestaurantId = 9
        }
    };
            return menuItems;
        }
    }
}
