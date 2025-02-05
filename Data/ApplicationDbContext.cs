using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySite.Models;

namespace MySite.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<BlogArticle> BlogArticles { get; set; }
        public DbSet<ReferenceProject> ReferenceProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Nezapomeò zavolat základní metodu!

            modelBuilder.Entity<ReferenceProject>().HasData(
                new ReferenceProject
                {
                    Id = 1,
                    Name = "Sádrokartony Trmota",
                    Link = "http://sadrokartonytrmota.cz/",
                    Description = "Moje první komerèní zakázka - webová stránka pro lokálního øemeslníka. Klient požadoval rychlost, jednoduchost pøehlednost a dobrou optimalizaci pro vyhledávaèe.", // Komerèní projekt nemá popis
                    Technologies = "Blazor, C#, Bootstrap, HTML, CSS",
                    ImagePath = "/images/content/sadrokartonyTrmota.webp",
                    IsCommercial = true
                },
                new ReferenceProject
                {
                    Id = 2,
                    Name = "Moje webové stránky",
                    Link = "http://davidbrach.cz/",
                    Description = "Moderní webová prezentace sloužící jako digitální vizitka. Pøedstavuje mé projekty, dovednosti a zkušenosti s dùrazem na èistý design a responzivitu.",
                    Technologies = "Blazor, C#, Bootstrap, HTML, CSS",
                    ImagePath = "/images/content/mojeStranky.webp",
                    IsCommercial = true
                },
                new ReferenceProject
                {
                    Id = 3,
                    Name = "Family Budget",
                    Link = "https://github.com/brachdavid/FamilyBudget",
                    Description = "Konzolová aplikace zamìøená na správu rodinného rozpoètu. Nabízí intuitivní rozhraní pro sledování pøíjmù a výdajù, analýzu dat a základní statistiky.",
                    Technologies = "C#, OOP, LINQ, Validace",
                    ImagePath = "/images/content/familyBudget.webp",
                    IsCommercial = false
                },
                new ReferenceProject
                {
                    Id = 4,
                    Name = "Jobseekers",
                    Link = "https://github.com/brachdavid/Jobseekers",
                    Description = "Konzolová aplikace pro evidenci uchazeèù o zamìstnání. Nauèila mì pracovat s databázemi pomocí Entity Framework Core a SQL Serveru.",
                    Technologies = "C#, OOP, Entity Framework Core, SQL Server",
                    ImagePath = "/images/content/jobseekers.webp",
                    IsCommercial = false
                },
                new ReferenceProject
                {
                    Id = 5,
                    Name = "Task Manager",
                    Link = "https://github.com/brachdavid/TaskManager",
                    Description = "Robustní webová aplikace umožòující správu úkolù, klientù a zamìstnancù. Postaveno s využitím Blazoru a napojením na databázi SQL Server.",
                    Technologies = "C#, Blazor, Entity Framework Core, SQL Server, Identity ASP.NET Core, HTML, CSS, Bootstrap",
                    ImagePath = "/images/content/taskManager.webp",
                    IsCommercial = false
                }
            );
        }
    }
}
