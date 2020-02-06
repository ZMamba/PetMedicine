using Microsoft.EntityFrameworkCore.Migrations;

namespace Animal_Medicine.Migrations
{
    public partial class PictureAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Diseases",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Diseases");
        }
    }
}
