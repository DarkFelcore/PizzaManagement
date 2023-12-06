using System.Security.Claims;

namespace PizzaManagement.Api.Extentions
{
    public static class AuthenticationExtentions
    {
        public static string GetEmailByClaimTypesAsync(ClaimsPrincipal claimPrincipal)
        {
            return claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)!.Value;
        }
    }
}