using BookShop.Infra;
using BookShop.WebUi.Mediator;
using BookShop.WebUi.Middleware;
using BookShop.WebUi.Models;
using BookShop.WebUi.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.ComponentModel.Design;
using System.Reflection;

namespace BookShop.WebUi
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
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("RedisSttings:ConnectionString");
                options.InstanceName = Configuration.GetValue<string>("RedisSttings:ChannelPrefix");
            });
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSession(option =>
            {
                option.Cookie.IsEssential = true;
                option.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddPaging(option => {
                option.ViewName = "Bootstrap4";
                option.HtmlIndicatorDown = " <span>&darr;</span>";
                option.HtmlIndicatorUp = " <span>&uarr;</span>";
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));

            services.AddHttpContextAccessor();

            services.Configure<AppSetting>(Configuration.GetSection("AppSetting"));

            
            services.AddTransient<IRedisService, RedisService>();

            services.AddScoped<ISelectListService, SelectListService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

            //IMiddlewareFactory must add transient
            services.AddTransient<ValidateTokenMiddleware>();
            //services.AddTransient<ValidateHeadersMiddleware>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            //app.UseMiddleware<ValidateTokenMiddleware>();
            app.UseMiddleware<ValidateHeadersMiddleware>();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Books}/{action=Index}/{id?}");
            });

            app.Run();
        }

    }
}
