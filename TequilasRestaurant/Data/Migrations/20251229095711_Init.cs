using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TequilasRestaurant.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductIngredients",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIngredients", x => new { x.ProductId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Безалкогольні та алкогольні напої різних видів", "Напої" },
                    { 2, "Хлібобулочні та кондитерські вироби", "Випічка" },
                    { 3, "Молоко, сири, йогурти та інші молочні вироби", "Молочні продукти" },
                    { 4, "Свіже м’ясо, ковбаси та м’ясні напівфабрикати", "М’ясні продукти" },
                    { 5, "Свіжа риба, морепродукти та рибні делікатеси", "Рибні продукти" },
                    { 6, "Свіжі сезонні овочі", "Овочі" },
                    { 7, "Свіжі фрукти та ягоди", "Фрукти" },
                    { 8, "Крупи, макаронні вироби та бобові", "Крупи та макарони" },
                    { 9, "Цукерки, шоколад, печиво та десерти", "Солодощі" },
                    { 10, "Заморожені овочі, напівфабрикати та морозиво", "Заморожені продукти" },
                    { 11, "Соуси, приправи та спеції", "Соуси та спеції" },
                    { 12, "Овочеві, м’ясні та рибні консерви", "Консерви" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Мелена кава з зерен арабіки", "Кава арабіка" },
                    { 2, "Коров’яче молоко для кавових напоїв", "Молоко" },
                    { 3, "Білий кристалічний цукор", "Цукор" },
                    { 4, "Пшеничне борошно", "Борошно" },
                    { 5, "Натуральне вершкове масло", "Вершкове масло" },
                    { 6, "Пекарські дріжджі", "Дріжджі" },
                    { 7, "М’ясо яловичини", "Яловичина" },
                    { 8, "Філе курячої грудки", "Куряче філе" },
                    { 9, "Філе лосося", "Лосось" },
                    { 10, "Паста з твердих сортів пшениці", "Паста" },
                    { 11, "Копчений бекон", "Бекон" },
                    { 12, "Вершки для соусів", "Вершки" },
                    { 13, "Курячі яйця", "Яйця" },
                    { 14, "Темний шоколад", "Шоколад" },
                    { 15, "Асорті свіжих фруктів", "Фрукти" },
                    { 16, "Сезонні овочі", "Овочі" },
                    { 17, "Оливкова олія першого віджиму", "Оливкова олія" },
                    { 18, "Кухонна харчова сіль", "Сіль" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Класичний еспресо", "Еспресо", 45.00m, 100 },
                    { 2, 1, "Кава з молочною пінкою", "Капучино", 65.00m, 80 },
                    { 3, 2, "Свіжа італійська чіабата", "Чіабата", 35.00m, 40 },
                    { 4, 2, "Теплий круасан з вершковим маслом", "Круасан з маслом", 55.00m, 30 },
                    { 5, 3, "Асорті твердих і м’яких сирів", "Сирна тарілка", 180.00m, 20 },
                    { 6, 4, "Стейк середньої прожарки", "Стейк з яловичини", 420.00m, 15 },
                    { 7, 4, "Філе курки на грилі", "Куряче філе гриль", 220.00m, 25 },
                    { 8, 5, "Філе лосося з лимоном", "Лосось на грилі", 380.00m, 12 },
                    { 9, 6, "Асорті сезонних овочів", "Овочі гриль", 160.00m, 30 },
                    { 10, 7, "Асорті свіжих фруктів", "Фруктова тарілка", 140.00m, 18 },
                    { 11, 8, "Паста з беконом і вершковим соусом", "Паста Карбонара", 260.00m, 22 },
                    { 12, 9, "Десерт з рідкою шоколадною начинкою", "Шоколадний фондан", 150.00m, 20 },
                    { 13, 11, "Домашній соус барбекю", "Соус BBQ", 40.00m, 50 }
                });

            migrationBuilder.InsertData(
                table: "ProductIngredients",
                columns: new[] { "IngredientId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 3 },
                    { 18, 3 },
                    { 4, 4 },
                    { 5, 4 },
                    { 6, 4 },
                    { 18, 4 },
                    { 2, 5 },
                    { 13, 5 },
                    { 7, 6 },
                    { 18, 6 },
                    { 8, 7 },
                    { 18, 7 },
                    { 9, 8 },
                    { 18, 8 },
                    { 16, 9 },
                    { 17, 9 },
                    { 18, 9 },
                    { 3, 10 },
                    { 15, 10 },
                    { 10, 11 },
                    { 11, 11 },
                    { 12, 11 },
                    { 13, 11 },
                    { 18, 11 },
                    { 3, 12 },
                    { 5, 12 },
                    { 13, 12 },
                    { 14, 12 },
                    { 17, 13 },
                    { 18, 13 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId1",
                table: "Orders",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductIngredients");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
