using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore1.Migrations
{
    public partial class updateClumeFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FillName",
                table: "Authors",
                newName: "FullName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Authors",
                newName: "FillName");
        }
    }
}
