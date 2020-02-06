using Microsoft.EntityFrameworkCore.Migrations;

namespace Animal_Medicine.Migrations
{
    public partial class PetTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PetTypeId",
                table: "Diseases",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_PetTypeId",
                table: "Diseases",
                column: "PetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diseases_PetTypes_PetTypeId",
                table: "Diseases",
                column: "PetTypeId",
                principalTable: "PetTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diseases_PetTypes_PetTypeId",
                table: "Diseases");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_PetTypeId",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "PetTypeId",
                table: "Diseases");
        }
    }
}
