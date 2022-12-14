namespace BookShop.WebUi.Middleware
{
    public class ValidateHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidateHeadersMiddleware(RequestDelegate next)
        {
            _next = next;   
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);
        }
    }
}
