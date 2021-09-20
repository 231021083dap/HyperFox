using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class Film : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GenreName",
                table: "Genre",
                type: "nvarchar(32)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldNullable: true);

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
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.FilmId);
                });

            migrationBuilder.InsertData(
                table: "Film",
                columns: new[] { "FilmId", "Description", "FilmName", "Image", "Price", "ReleaseDate", "RuntimeInMin", "Stock" },
                values: new object[] { 1, "This movie is about a ring", "The lord of the rings", "C:\\Users\\Tec\\Pictures\\1.jpg", 79.99m, "16-09-2001", (short)123, (short)50 });

            migrationBuilder.InsertData(
                table: "Film",
                columns: new[] { "FilmId", "Description", "FilmName", "Image", "Price", "ReleaseDate", "RuntimeInMin", "Stock" },
                values: new object[] { 2, "This movie is about the wizard world", "Harry potter", "C:\\Users\\Tec\\Pictures\\2.jpg", 79.99m, "16-09-2001", (short)123, (short)50 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.AlterColumn<string>(
                name: "GenreName",
                table: "Genre",
                type: "nvarchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)");
        }
    }
}
