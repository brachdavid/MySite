using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class BlogArticleImageSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageSource",
                table: "BlogArticles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ReferenceProjects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImagePath", "Name" },
                values: new object[] { "/images/content/mojeStranky.webp", "Moje webové stránky" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageSource",
                table: "BlogArticles");

            migrationBuilder.UpdateData(
                table: "ReferenceProjects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ImagePath", "Name" },
                values: new object[] { "/images/content/docasna.webp", "Osobní portfolio" });
        }
    }
}
