using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondHandBook.Migrations
{
    public partial class imgtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.AddColumn<string>(
                name: "OfferDescription",
                table: "BookOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BookOfferImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookOfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOfferImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookOfferImages_BookOffers_BookOfferId",
                        column: x => x.BookOfferId,
                        principalTable: "BookOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookOfferImages_BookOfferId",
                table: "BookOfferImages",
                column: "BookOfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookOfferImages");

            migrationBuilder.DropColumn(
                name: "OfferDescription",
                table: "BookOffers");

            migrationBuilder.CreateTable(
                name: "BookOfferDto",
                columns: table => new
                {
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfOffer = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiverId = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PagesCount = table.Column<int>(type: "int", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }
    }
}
