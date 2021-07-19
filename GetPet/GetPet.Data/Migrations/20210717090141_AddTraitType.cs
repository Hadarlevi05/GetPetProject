using Microsoft.EntityFrameworkCore.Migrations;

namespace GetPet.Data.Migrations
{
    public partial class AddTraitType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TraitType",
                table: "Traits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TraitType",
                table: "Traits");
        }
    }
}
