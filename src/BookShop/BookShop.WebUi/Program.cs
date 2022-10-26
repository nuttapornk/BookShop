using BookShop.Infra;
using BookShop.WebUi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
	try
	{
        var context = service.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
	catch (Exception ex)
	{
		var logger = service.GetRequiredService<ILogger<Program>>();
		logger.LogError(ex, "An Error occurred sedding the Database.");
	}
}

startup.Configure(app, builder.Environment);
