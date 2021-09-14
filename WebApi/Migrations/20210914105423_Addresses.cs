using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Addresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address",
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
                    table.PrimaryKey("PK_address", x => x.AddressId);
                });

            migrationBuilder.InsertData(
                table: "address",
                columns: new[] { "AddressId", "Add", "City", "Postal", "UserId" },
                values: new object[] { 1, "Tec Ballerup", "Ballerup", 2700, 0 });

            migrationBuilder.InsertData(
                table: "address",
                columns: new[] { "AddressId", "Add", "City", "Postal", "UserId" },
                values: new object[] { 2, "Tec Ballerup", "Ballerup", 2700, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");
        }
    }
}
