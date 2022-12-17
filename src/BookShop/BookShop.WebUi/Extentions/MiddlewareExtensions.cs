using BookShop.WebUi.Middleware;

namespace BookShop.WebUi.Extentions
{
    public static class MiddlewareExtensions
    {
        ///full format
        //public static IApplicationBuilder UseValidateTokenMiddleware(this IApplicationBuilder app)
        //{
        //   return app.UseMiddleware<ValidateTokenMiddleware>();     
        //}

        public static IApplicationBuilder UseValidateTokenMiddleware(this IApplicationBuilder app)
            => (WebApplication)app.UseMiddleware<ValidateTokenMiddleware>();

        //public static WebApplication UseValidateHeadersMiddleware(this WebApplication app, bool option = false)
        //{
        //    return (WebApplication)app.UseMiddleware<ValidateHeadersMiddleware>(option);
        //}



    }
}
