using Microsoft.EntityFrameworkCore.Migrations;

namespace GetPet.Data.Migrations
{
    public partial class IsBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBoolean",
                table: "Traits",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBoolean",
                table: "Traits");
        }
    }
}
