using BookShop.Infra;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using System.Reflection;
using BookShop.Common;
using BookShop.WebApi2.Process;

namespace BookShop.WebApi2
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSwaggerGen();

            services.AddHttpContextAccessor();
            services.AddSingleton<DapperContext>();
            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            string pathBase = Environment.GetEnvironmentVariable("PATH_BASE");

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase("/" + pathBase);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            //app.UseAuthentication();
            app.MapControllers();
            app.Run();
        }
    }
}
