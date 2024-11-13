using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TastyOrders.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "ImageUrl", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "/images/smashBurger.png", "Sofia", "Smash Burgers" },
                    { 2, "/images/italyRestaurant.jpg", "Varna", "Taste of Italy" },
                    { 3, "/images/sweetPlace.jpg", "Veliko Tarnovo", "Sweet Place" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Delicious plant-based burger", "/images/veggieBurger.jpg", "Veggie Burger", 12.99m, 1 },
                    { 2, "Delicious burger with fresh meat", "/images/beefBurger.jpg", "Beef Burger", 18.99m, 1 },
                    { 3, "Delicious burger with chicken and cheese", "/images/chickenBurger.jpg", "Chicken Burger", 15.99m, 1 },
                    { 4, "Classic Italian pasta with rich sauce", "/images/meatPasta.jpg", "Spaghetti Bolognese", 16.50m, 2 },
                    { 5, "Classic pizza with tomato, basil, and mozzarella", "/images/pizzaM.jpg", "Margherita Pizza", 11.99m, 2 },
                    { 6, "Fresh lettuce with Caesar dressing", "/images/ceaserSalad.jpg", "Caesar Salad", 10.99m, 2 },
                    { 7, "Our best seller", "/images/cheesecake.jpg", "Cheesecake", 8.99m, 3 },
                    { 8, "The richest chocolate taste you can find", "/images/chokoCake.jpg", "Chocolate cake", 7.99m, 3 },
                    { 9, "Tastiest cream brulee you can find", "/images/creamB.jpg", "Cream Brulee", 5.99m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
