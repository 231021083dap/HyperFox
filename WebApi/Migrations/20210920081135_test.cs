using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Film",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Film",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Film",
                keyColumn: "FilmId",
                keyValue: 1,
                column: "GenreId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Film",
                keyColumn: "FilmId",
                keyValue: 2,
                column: "GenreId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Film_GenreId",
                table: "Film",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Film_Genre_GenreId",
                table: "Film",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Film_Genre_GenreId",
                table: "Film");

            migrationBuilder.DropIndex(
                name: "IX_Film_GenreId",
                table: "Film");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Film");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Film",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");
        }
    }
}
