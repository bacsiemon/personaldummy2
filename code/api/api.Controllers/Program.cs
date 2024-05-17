
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.Entities;
using Services.JWT;
using Services.JWT.Impl;
using Services.User;
using Services.User.Impl;

namespace api.Controllers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();

            // cors
            var MyAllowSpecificOrigins = "api";
            ServiceConfig.ConfigureCors(builder, MyAllowSpecificOrigins);

            ServiceConfig.ConfigureNewtonsoftJson(builder);

            ServiceConfig.ConfigureDbContext(builder);

            ServiceConfig.ConfigureIdentity(builder);

            ServiceConfig.ConfigureAuthorization(builder);

            ServiceConfig.ConfigureSwaggerGen(builder);

            ServiceConfig.ConfigureAuthentication(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        

        
    }
}
