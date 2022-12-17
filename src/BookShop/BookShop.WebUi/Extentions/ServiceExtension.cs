
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;

namespace BookShop.WebUi.Extentions
{
    public static class ServiceExtension
    {
        public static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(cookie =>
            {
                cookie.Cookie.Name = "keycloak.cookie";
                cookie.Cookie.MaxAge = TimeSpan.FromMinutes(60);
                cookie.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                cookie.SlidingExpiration = true;
            }).AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = configuration["Keycloak:ServerRealm"];
                options.ClientId = configuration["Keycloak:ClientId"];
                options.ClientSecret = configuration["Keycloak:ClientSecret"];
                options.MetadataAddress = configuration["Keycloak:MetaData"];
                options.RequireHttpsMetadata = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.SaveTokens = true;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.NonceCookie.SameSite = SameSiteMode.Unspecified;
                options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuer = true
                };
            });

        }


    }
}
