using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySite.Data;

namespace MySite.Components.Account
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var accountGroup = endpoints.MapGroup("/admin"); // Pøejmenování podle nového stylu URL

            accountGroup.MapPost("/odhlaseni", async (
                ClaimsPrincipal user,
                SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect("~/");
            });

            return accountGroup;
        }
    }
}
