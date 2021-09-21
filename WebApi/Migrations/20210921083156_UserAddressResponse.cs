using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class UserAddressResponse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_AddressId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AddressId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Add",
                table: "Address",
                newName: "StreetName");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_User_UserId",
                table: "Address",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_User_UserId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_UserId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "StreetName",
                table: "Address",
                newName: "Add");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_AddressId",
                table: "User",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_AddressId",
                table: "User",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
