using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Work : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    Admin = table.Column<string>(type: "nvarchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmName = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    ReleaseDate = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    RuntimeInMin = table.Column<short>(type: "smallInt", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Stock = table.Column<short>(type: "smallInt", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.FilmId);
                    table.ForeignKey(
                        name: "FK_Film_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetName = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Postal = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[] { 1, "Comedy" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Admin", "Email", "Password", "UserName" },
                values: new object[] { 1, "Admin", "TestMail", "TestPassword", "TestUserName" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Admin", "Email", "Password", "UserName" },
                values: new object[] { 2, "User", "Test2", "Test2", "Test2" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "City", "Postal", "StreetName", "UserId" },
                values: new object[,]
                {
                    { 1, "Ballerup", 2700, "Tec Ballerup", 1 },
                    { 2, "Kattegat", 2700, "Havet", 2 }
                });

            migrationBuilder.InsertData(
                table: "Film",
                columns: new[] { "FilmId", "Description", "FilmName", "GenreId", "Image", "Price", "ReleaseDate", "RuntimeInMin", "Stock" },
                values: new object[,]
                {
                    { 1, "This movie is about a ring", "The lord of the rings", 1, "https://www.information.dk/sites/information.dk/files/styles/open_graph/public/media/2011/05/27/20110527-144201-124983_0.jpg?itok=dpVGfrig", 79.99m, "16-09-2001", (short)123, (short)50 },
                    { 2, "This movie is about the wizard world", "Harry potter", 1, "https://www.bog-ide.dk/scommerce/images/klods-hans-592756.jpg?i=592756", 79.99m, "16-09-2001", (short)123, (short)50 }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "DateTime", "UserId" },
                values: new object[,]
                {
                    { 1, "2001-08-21 04:45:21", 1 },
                    { 2, "2001-08-21 04:45:41", 1 }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "FilmId", "OrderId", "Price", "Quantity" },
                values: new object[] { 1, 1, 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "FilmId", "OrderId", "Price", "Quantity" },
                values: new object[] { 2, 2, 2, 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Film_GenreId",
                table: "Film",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_FilmId",
                table: "Item",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_OrderId",
                table: "Item",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
