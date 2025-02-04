using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class ReferenceProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReferenceProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Technologies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCommercial = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceProjects", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ReferenceProjects",
                columns: new[] { "Id", "Description", "ImagePath", "IsCommercial", "Link", "Name", "Technologies" },
                values: new object[,]
                {
                    { 1, "Moje první komerční zakázka - webová stránka pro lokálního řemeslníka. Klient požadoval rychlost, jednoduchost přehlednost a dobrou optimalizaci pro vyhledávače.", "/images/content/sadrokartonyTrmota.webp", true, "http://sadrokartonytrmota.cz/", "Sádrokartony Trmota", "Blazor, C#, Bootstrap, HTML, CSS" },
                    { 2, "Moderní webová prezentace sloužící jako digitální vizitka. Představuje mé projekty, dovednosti a zkušenosti s důrazem na čistý design a responzivitu.", "/images/content/docasna.webp", true, "http://davidbrach.cz/", "Osobní portfolio", "Blazor, C#, Bootstrap, HTML, CSS" },
                    { 3, "Konzolová aplikace zaměřená na správu rodinného rozpočtu. Nabízí intuitivní rozhraní pro sledování příjmů a výdajů, analýzu dat a základní statistiky.", "/images/content/familyBudget.webp", false, "https://github.com/brachdavid/FamilyBudget", "Family Budget", "C#, OOP, LINQ, Validace" },
                    { 4, "Konzolová aplikace pro evidenci uchazečů o zaměstnání. Naučila mě pracovat s databázemi pomocí Entity Framework Core a SQL Serveru.", "/images/content/jobseekers.webp", false, "https://github.com/brachdavid/Jobseekers", "Jobseekers", "C#, OOP, Entity Framework Core, SQL Server" },
                    { 5, "Robustní webová aplikace umožňující správu úkolů, klientů a zaměstnanců. Postaveno s využitím Blazoru a napojením na databázi SQL Server.", "/images/content/taskManager.webp", false, "https://github.com/brachdavid/TaskManager", "Task Manager", "C#, Blazor, Entity Framework Core, SQL Server, Identity ASP.NET Core, HTML, CSS, Bootstrap" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferenceProjects");
        }
    }
}
