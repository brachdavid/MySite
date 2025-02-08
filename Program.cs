using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.ResponseCompression; // Přidáno pro kompresi
using MySite.Components;
using MySite.Components.Account;
using MySite.Data;
using MySite.Services;
using System.Linq;
using MySite.Models;
using System.Security.Cryptography;
using System.IO.Compression; // Přidáno pro úrovně komprese

namespace MySite
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Přidání podpory pro kompresi odpovědí
            builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true; // Komprese i pro HTTPS
                options.Providers.Add<BrotliCompressionProvider>(); // Brotli komprese
                options.Providers.Add<GzipCompressionProvider>(); // Gzip komprese
            });

            // ✅ Nastavení úrovně komprese (nejlepší kvalita)
            builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            // ✅ Standardní služby
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

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/prihlaseni";
                options.LogoutPath = "/odhlaseni";
                options.AccessDeniedPath = "/";
            });

            // SMTP nastavení
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            builder.Services.AddSingleton<IEmailSender, MailKitEmailSender>();

            var app = builder.Build();

            // ✅ Aktivace middleware pro kompresi
            app.UseResponseCompression();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();

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
                            throw new Exception($"Administrátorský uživatel nemohl být vytvořen: {errors}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Chyba při seedování administrátorského uživatele.");
                    throw;
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                var nonce = Convert.ToBase64String(RandomNumberGenerator.GetBytes(16)); // Generuje bezpečný nonce
                var headers = context.Response.Headers;

                headers.ContentSecurityPolicy =
                    "default-src 'self'; " +
                    "script-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net; " +
                    "style-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net https://fonts.googleapis.com https://cdn.jsdelivr.net/npm/bootstrap-icons/; " +
                    "font-src 'self' https://fonts.gstatic.com https://cdn.jsdelivr.net/npm/bootstrap-icons/; " +
                    "img-src 'self' data: https://toplist.cz https://www.davidbrach.cz; " +
                    "connect-src 'self' https://www.davidbrach.cz ws://localhost:* http://localhost:* wss://www.davidbrach.cz; " +
                    "frame-ancestors 'self';";

                headers.XFrameOptions = "SAMEORIGIN";
                headers.XContentTypeOptions = "nosniff";
                headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
                headers["Permissions-Policy"] = "geolocation=(), microphone=(), camera=()";

                context.Items["CSPNonce"] = nonce;
                await next();
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapAdditionalIdentityEndpoints();

            await app.RunAsync();
        }
    }
}
