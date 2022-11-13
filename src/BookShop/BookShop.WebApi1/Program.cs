using BookShop.WebApi1;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Environment);

startup.ConfigureServices(builder.Services);
startup.Configure(builder.Build(), builder.Environment);