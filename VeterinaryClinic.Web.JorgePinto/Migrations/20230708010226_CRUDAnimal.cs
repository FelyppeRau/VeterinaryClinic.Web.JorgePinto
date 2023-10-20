using Microsoft.EntityFrameworkCore.Migrations;

namespace VeterinaryClinic.Web.JorgePinto.Migrations
{
    public partial class CRUDAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Race",
                table: "Animals",
                newName: "Breed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Breed",
                table: "Animals",
                newName: "Race");
        }
    }
}
