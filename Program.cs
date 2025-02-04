using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using MySite.Components;
using MySite.Components.Account;
using MySite.Data;
using MySite.Services; // Pøedpokládáme, že tøídy MailKitEmailSender a SmtpSettings jsou zde
using System.Linq;
using MySite.Models;

namespace MySite
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            // Zde pøidáváme konfiguraci pøihlašovací stránky
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/prihlaseni"; // Pøesmìrování na vlastní login stránku
                options.LogoutPath = "/odhlaseni";
                options.AccessDeniedPath = "/"; // Kam poslat uživatele bez oprávnìní
            });

            // Starou implementaci nahrazujeme novou registrací:
            // builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            // Naètení SMTP nastavení z konfigurace (sekce "SmtpSettings" v appsettings.json)
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            // Registrace univerzálního email senderu založeného na MailKit
            builder.Services.AddSingleton<IEmailSender, MailKitEmailSender>();

            var app = builder.Build();

            // Provedení migrací a seed administrátorského uživatele
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    // Aplikace migrací
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();

                    // Seed administrátorského uživatele
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var adminEmail = builder.Configuration["AdminUser:Email"];
                    var adminPassword = builder.Configuration["AdminUser:Password"];

                    if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
                    {
                        throw new Exception("Konfigurace AdminUser (Email nebo Password) nebyla nastavena.");
                    }

                    var adminUser = await userManager.FindByEmailAsync(adminEmail);
                    if (adminUser == null)
                    {
                        adminUser = new ApplicationUser
                        {
                            UserName = adminEmail,
                            Email = adminEmail,
                            EmailConfirmed = true
                        };

                        var result = await userManager.CreateAsync(adminUser, adminPassword);
                        if (!result.Succeeded)
                        {
                            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                            throw new Exception($"Administrátorský uživatel nemohl být vytvoøen: {errors}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Chyba pøi seedování administrátorského uživatele.");
                    throw;
                }
            }

            // Konfigurace HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // Výchozí hodnota HSTS je 30 dní; pro produkci to mùžeš dle potøeby zmìnit.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Pøidání dalších endpointù potøebných pro Identity /Account Razor komponenty.
            app.MapAdditionalIdentityEndpoints();

            await app.RunAsync();
        }
    }
}
