using ClinicManagementWebPage.Interfaces;
using ClinicManagementWebPage.Models;
using ClinicManagementWebPage.Repositories;
using ClinicManagementWebPage.Services;

namespace ClinicManagementWebPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSession();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region UserService Injection
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            #endregion
            
            #region DoctorService Injection
            builder.Services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            #endregion

            #region AppointmentService Injection
            builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            #endregion

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
                pattern: "{controller=User}/{action=Login}/{id?}");

            app.Run();
        }
    }
}