using EventBookingWebApi.Context;
using EventBookingWebApi.Interfaces;
using EventBookingWebApi.Models;
using EventBookingWebApi.Repositories;
using EventBookingWebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace EventBookingWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Contexts
            builder.Services.AddDbContext<EventContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion


            #region
            builder.Services.AddAutoMapper(typeof(Employee));
            //builder.Services.AddAutoMapper(typeof(ProductImage));
            #endregion


            #region Repositories
            builder.Services.AddScoped<IRepository<int, Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IRepository<int, Event>, EventRepository>();
            builder.Services.AddScoped<IRepository<int, Booking>, BookingRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IEventService, EventService>();
            #endregion

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
