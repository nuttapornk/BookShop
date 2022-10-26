using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Infra.Migrations
{
    public partial class RemoveAkBookIsbn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Book_Isbn",
                table: "Book");

            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Book",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Book",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Book_Isbn",
                table: "Book",
                column: "Isbn");
        }
    }
}
