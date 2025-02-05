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
            base.OnModelCreating(modelBuilder); // Nezapome� zavolat z�kladn� metodu!

            modelBuilder.Entity<ReferenceProject>().HasData(
                new ReferenceProject
                {
                    Id = 1,
                    Name = "S�drokartony Trmota",
                    Link = "http://sadrokartonytrmota.cz/",
                    Description = "Moje prvn� komer�n� zak�zka - webov� str�nka pro lok�ln�ho �emesln�ka. Klient po�adoval rychlost, jednoduchost p�ehlednost a dobrou optimalizaci pro vyhled�va�e.", // Komer�n� projekt nem� popis
                    Technologies = "Blazor, C#, Bootstrap, HTML, CSS",
                    ImagePath = "/images/content/sadrokartonyTrmota.webp",
                    IsCommercial = true
                },
                new ReferenceProject
                {
                    Id = 2,
                    Name = "Moje webov� str�nky",
                    Link = "http://davidbrach.cz/",
                    Description = "Modern� webov� prezentace slou��c� jako digit�ln� vizitka. P�edstavuje m� projekty, dovednosti a zku�enosti s d�razem na �ist� design a responzivitu.",
                    Technologies = "Blazor, C#, Bootstrap, HTML, CSS",
                    ImagePath = "/images/content/mojeStranky.webp",
                    IsCommercial = true
                },
                new ReferenceProject
                {
                    Id = 3,
                    Name = "Family Budget",
                    Link = "https://github.com/brachdavid/FamilyBudget",
                    Description = "Konzolov� aplikace zam��en� na spr�vu rodinn�ho rozpo�tu. Nab�z� intuitivn� rozhran� pro sledov�n� p��jm� a v�daj�, anal�zu dat a z�kladn� statistiky.",
                    Technologies = "C#, OOP, LINQ, Validace",
                    ImagePath = "/images/content/familyBudget.webp",
                    IsCommercial = false
                },
                new ReferenceProject
                {
                    Id = 4,
                    Name = "Jobseekers",
                    Link = "https://github.com/brachdavid/Jobseekers",
                    Description = "Konzolov� aplikace pro evidenci uchaze�� o zam�stn�n�. Nau�ila m� pracovat s datab�zemi pomoc� Entity Framework Core a SQL Serveru.",
                    Technologies = "C#, OOP, Entity Framework Core, SQL Server",
                    ImagePath = "/images/content/jobseekers.webp",
                    IsCommercial = false
                },
                new ReferenceProject
                {
                    Id = 5,
                    Name = "Task Manager",
                    Link = "https://github.com/brachdavid/TaskManager",
                    Description = "Robustn� webov� aplikace umo��uj�c� spr�vu �kol�, klient� a zam�stnanc�. Postaveno s vyu�it�m Blazoru a napojen�m na datab�zi SQL Server.",
                    Technologies = "C#, Blazor, Entity Framework Core, SQL Server, Identity ASP.NET Core, HTML, CSS, Bootstrap",
                    ImagePath = "/images/content/taskManager.webp",
                    IsCommercial = false
                }
            );
        }
    }
}
