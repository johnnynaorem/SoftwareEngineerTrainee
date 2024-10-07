using UnderstandingStructureApp.Interface;
using UnderstandingStructureApp.Models;
using UnderstandingStructureApp.Repositories;
using UnderstandingStructureApp.Services;

namespace UnderstandingStructureApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPizzaService, PizzaService>(); //service will be used in controller
            builder.Services.AddScoped<IRepository<int, PizzaImages>, PizzaImageRepository>(); //Rep will be used in service
            builder.Services.AddScoped<IRepository<int, Pizza>, PizzaRepository>(); //Rep will be used in service
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Pizza}/{action=Index}/{id?}");

            app.Run();
        }
    }
}