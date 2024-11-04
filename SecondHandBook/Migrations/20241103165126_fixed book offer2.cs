using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondHandBook.Migrations
{
    public partial class fixedbookoffer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTaken",
                table: "BookOffers",
                newName: "IsCollected");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCollected",
                table: "BookOffers",
                newName: "IsTaken");
        }
    }
}
