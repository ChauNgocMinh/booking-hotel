using HotelManagement.DataAccess;
using HotelManagement.Models;
using HotelManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddHttpContextAccessor();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<HotelContext>(option =>
            {
                option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            });


            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IVnPayService, VnPayService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}",
                defaults: new {controller= "Home",action = "Index"});

            app.Run();
        }
    }
}