
using Microsoft.EntityFrameworkCore;
using ParkingManagement.BL;
using ParkingManagement.DAL;
using ParkingManagement.DAL.Models;
using ParkingManagement.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ParkingManagement.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                             
                });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddScoped<IBL, BL.BL>();
            builder.Services.AddScoped<IDAL, DAL.DAL>();


            builder.Services.AddDbContext<ParkingManagementContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("ParkingListDb"))
            );


            builder.Services.AddSingleton<ILog>(sp =>
            {
                var fileFolderPath = builder.Configuration.GetConnectionString("LogFileLocation");
                return new Log(fileFolderPath);
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://127.0.0.1:5500") // Adjust this to the origin of your web application
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .AllowAnyMethod();
            });
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
