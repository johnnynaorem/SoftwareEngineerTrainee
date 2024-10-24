using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Contexts;
using PolicyClaimWebApi.Interfaces;
using PolicyClaimWebApi.Models;
using PolicyClaimWebApi.Repositories;
using PolicyClaimWebApi.Services;

namespace PolicyClaimWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Contexts
            builder.Services.AddDbContext<PolicyContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Repositories
            builder.Services.AddScoped<IRepository<int, Claimant>, ClaimantRepository>();
            builder.Services.AddScoped<IRepository<int, Claim>, ClaimRepository>();
            builder.Services.AddScoped<IRepository<int, Policy>, PolicyRepository>();
            builder.Services.AddScoped<IRepository<int, ClaimType>, ClaimTypeRepository>();
            builder.Services.AddScoped<IRepository<int, ClaimFile>, ClaimFileRepository>();
            #endregion

            #region Services    
            builder.Services.AddScoped<IClaimantService, ClaimantService>();
            builder.Services.AddScoped<IClaimService, ClaimService>();
            builder.Services.AddScoped<IPolicyService, PolicyService>();
            #endregion;
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
