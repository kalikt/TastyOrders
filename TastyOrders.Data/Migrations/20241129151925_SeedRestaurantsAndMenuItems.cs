using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TastyOrders.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRestaurantsAndMenuItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "ImageUrl", "Location", "Name" },
                values: new object[,]
                {
                    { 4, "/images/soupAndSaladRestaurant.jpg", "Sofia", "Soup & Salad" },
                    { 5, "/images/tacoSpot.jpg", "Sofia", "Taco Spot" },
                    { 6, "/images/sunnyDiner.jpg", "Varna", "Sunny Diner" },
                    { 7, "/images/endorfino.jpg", "Varna", "Endorfino" },
                    { 8, "/images/pastaPlace.jpg", "Veliko Tarnovo", "Pasta Place" },
                    { 9, "/images/pizzaPlace.jpg", "Veliko Tarnovo", "Pizza Corner" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 10, "A creamy and flavorful tomato soup with fresh basil.", "/images/tomatoBasilSoup.jpg", "Tomato Basil Soup", 5.49m, 4 },
                    { 11, "Loaded salad with chicken, bacon, avocado, eggs, and blue cheese.", "/images/cobbSalad.jpg", "Cobb Salad", 9.99m, 4 },
                    { 12, "Rich onion soup topped with a layer of melted cheese and croutons.", "/images/frenchOnionSoup.jpg", "French Onion Soup", 7.99m, 4 },
                    { 13, "Soft tacos filled with seasoned chicken and fresh toppings.", "/images/chickenTacos.jpg", "Chicken Tacos", 9.99m, 5 },
                    { 14, "Crispy battered fish served with cabbage slaw and a zesty lime crema.", "/images/fishTacos.jpg", "Fish Tacos", 11.49m, 5 },
                    { 15, "Slow-cooked beef barbacoa with fresh cilantro, onions, and lime.", "/images/beefBarbacoaTacos.jpg", "Beef Barbacoa Tacos", 8.99m, 5 },
                    { 16, "Perfectly grilled salmon served with a side of vegetables.", "/images/grilledSalmon.jpg", "Grilled Salmon", 15.99m, 6 },
                    { 17, "Juicy shrimp cooked in a garlic butter sauce, served over linguine.", "/images/shrimpScampi.jpg", "Shrimp Scampi", 19.99m, 6 },
                    { 18, "Grilled steak served with a side of crispy French fries.", "/images/steakFrites.jpg", "Steak Frites", 18.99m, 6 },
                    { 19, "Rich chocolate brownie topped with vanilla ice cream.", "/images/chocolateBrownie.jpg", "Chocolate Brownie", 5.99m, 7 },
                    { 20, "Stack of fluffy pancakes served with syrup and butter.", "/images/classicPancakes.jpg", "Classic Pancakes", 5.49m, 7 },
                    { 21, "Warm chocolate cake with a gooey molten center, served with ice cream.", "/images/chocolateLavaCake.jpg", "Chocolate Lava Cake", 6.99m, 7 },
                    { 22, "Creamy Alfredo sauce over perfectly cooked fettuccine.", "/images/fettuccineAlfredo.jpg", "Fettuccine Alfredo", 12.99m, 8 },
                    { 23, "Classic Italian pasta with creamy egg sauce, pancetta, and Parmesan cheese.", "/images/spaghettiCarbonara.jpg", "Spaghetti Carbonara", 12.99m, 8 },
                    { 24, "Spicy tomato-based pasta dish with penne, garlic, and red chili flakes.", "/images/penneArrabbiata.jpg", "Penne Arrabbiata", 11.49m, 8 },
                    { 25, "Classic pizza topped with pepperoni and melted mozzarella cheese.", "/images/pepperoniPizza.jpg", "Pepperoni Pizza", 13.99m, 9 },
                    { 26, "A blend of mozzarella, cheddar, Parmesan, and blue cheese on a crispy crust.", "/images/fourCheesePizza.jpg", "Four Cheese Pizza", 11.99m, 9 },
                    { 27, "Tender chicken, BBQ sauce, red onions, and cilantro on a wood-fired crust.", "/images/bbqChickenPizza.jpg", "BBQ Chicken Pizza", 13.49m, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
