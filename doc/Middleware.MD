
# Factory-based middleware activation in ASP.NET Core

[Reference](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/extensibility?view=aspnetcore-6.0)

`IMiddlewareFactory`/`IMiddleware` is an extensibility point for middleware activation that offers the following benefits:

- Activation per client request (injection of scoped services)
- Strong typing of middleware

## Middleware activated by convention:
```Csharp
public class ConventionalMiddleware
{
    private readonly RequestDelegate _next;

    public ConventionalMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(HttpContext context, SampleDbContext dbContext)
    {
        var keyValue = context.Request.Query["key"];
        if (!string.IsNullOrWhiteSpace(keyValue))
        {
            dbContext.Requests.Add(new Request("Conventional", keyValue));
            await dbContext.SaveChangesAsync();
        }
        await _next(context);
    }
}
```
## Middleware activated by MiddlewareFactory:
```Javascript
public class FactoryActivatedMiddleware : IMiddleware
{
    private readonly SampleDbContext _dbContext;

    public FactoryActivatedMiddleware(SampleDbContext dbContext)
        => _dbContext = dbContext;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var keyValue = context.Request.Query["key"];
        if (!string.IsNullOrWhiteSpace(keyValue))
        {
            _dbContext.Requests.Add(new Request("Factory", keyValue));
            await _dbContext.SaveChangesAsync();
        }

        await next(context);
    }
}
```

Extensions are created for the middleware:
```Javascript
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseConventionalMiddleware(
        this IApplicationBuilder app)
        => app.UseMiddleware<ConventionalMiddleware>();

    public static IApplicationBuilder UseFactoryActivatedMiddleware(
        this IApplicationBuilder app)
        => app.UseMiddleware<FactoryActivatedMiddleware>();
}
```

The factory-activated middleware is added to the built-in container in Program.cs
```Javascript
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SampleDbContext>
    (options => options.UseInMemoryDatabase("SampleDb"));

builder.Services.AddTransient<FactoryActivatedMiddleware>();
```

Both middleware are registered in the request processing pipeline, also in
```Javascript
var app = builder.Build();

app.UseConventionalMiddleware();
app.UseFactoryActivatedMiddleware();
```