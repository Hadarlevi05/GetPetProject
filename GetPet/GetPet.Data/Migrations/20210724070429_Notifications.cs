using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GetPet.Data.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AnimalTypes_AnimalTypeId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationTraits");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AnimalTypeId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AnimalTypeId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "PetFilterSerialized",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotificationId",
                table: "EmailHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetFilterSerialized",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationId",
                table: "EmailHistories");

            migrationBuilder.AddColumn<int>(
                name: "AnimalTypeId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificationTraits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    TraitId = table.Column<int>(type: "int", nullable: false),
                    TraitOptionId = table.Column<int>(type: "int", nullable: false),
                    UpdatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTraits_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationTraits_TraitOptions_TraitOptionId",
                        column: x => x.TraitOptionId,
                        principalTable: "TraitOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationTraits_Traits_TraitId",
                        column: x => x.TraitId,
                        principalTable: "Traits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AnimalTypeId",
                table: "Notifications",
                column: "AnimalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTraits_NotificationId",
                table: "NotificationTraits",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTraits_TraitId",
                table: "NotificationTraits",
                column: "TraitId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTraits_TraitOptionId",
                table: "NotificationTraits",
                column: "TraitOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AnimalTypes_AnimalTypeId",
                table: "Notifications",
                column: "AnimalTypeId",
                principalTable: "AnimalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
