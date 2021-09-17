using Microsoft.EntityFrameworkCore.Migrations;


namespace WebApi.Migrations
{
    public partial class Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Add = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Postal = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(40)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
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
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "Add", "City", "Postal", "UserId" },
                values: new object[,]
                {
                    { 1, "Tec Ballerup", "Ballerup", 2700, 0 },
                    { 2, "Tec Ballerup", "Ballerup", 2700, 0 }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "DateTime", "UserId" },
                values: new object[,]
                {
                    { 1, "Friday 13th at 4:00", 1 },
                    { 2, "Friday 13th at 4:00", 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
