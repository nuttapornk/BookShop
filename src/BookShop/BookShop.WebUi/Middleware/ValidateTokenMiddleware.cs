using BookShop.Common;
using Microsoft.Extensions.Options;

namespace BookShop.WebUi.Middleware
{
    public class ValidateTokenMiddleware : IMiddleware
    {
        private readonly AppSetting _appSetting;
        public ValidateTokenMiddleware(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            await next(context);


        }
    }
}
