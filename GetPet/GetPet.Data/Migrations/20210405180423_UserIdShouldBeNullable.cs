using Microsoft.EntityFrameworkCore.Migrations;

namespace GetPet.Data.Migrations
{
    public partial class UserIdShouldBeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Pets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Users_UserId",
                table: "Pets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
