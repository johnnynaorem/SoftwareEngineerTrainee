using InsurancePolicyWebAPI.Context;
using InsurancePolicyWebAPI.Interfaces;
using InsurancePolicyWebAPI.Models;
using InsurancePolicyWebAPI.Repositories;
using InsurancePolicyWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InsurancePolicyWebAPI
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
            builder.Services.AddScoped<IRepository<int, ClaimReport>, ClaimReportRepository>();
            builder.Services.AddScoped<IRepository<int, Claim>, ClaimRepository>();
            builder.Services.AddScoped<IRepository<int, Policy>, PolicyRepository>();
            builder.Services.AddScoped<IRepository<int, Policy>, PolicyRepository>();

            #endregion

            #region Services    
            builder.Services.AddScoped<IClaimantService, ClaimantService>();
            builder.Services.AddScoped<IClaimReport, ClaimReportService>();
            builder.Services.AddScoped<IClaimService, ClaimService>();
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
