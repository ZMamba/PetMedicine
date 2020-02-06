using Microsoft.EntityFrameworkCore.Migrations;

namespace Animal_Medicine.Migrations
{
    public partial class addTagSimptomsToDiseas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Diseases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Diseases");
        }
    }
}
