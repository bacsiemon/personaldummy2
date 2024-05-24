using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.Entities;
using Repositories.UOW;
using Repositories.UOW.Impl;
using Services.DepartmentServices;
using Services.DepartmentServices.Impl;
using Services.EmployeeServices;
using Services.EmployeeServices.Impl;
using Services.JWT;
using Services.JWT.Impl;
using Services.UserServices;
using Services.UserServices.Impl;

namespace api.Controllers
{
    public static class ServiceConfig
    {

        public static void ConfigureDependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();  
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
        }

        public static void ConfigureAuthorization(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("HumanResourceOrHigher", policy => policy.RequireClaim("HumanResource").RequireClaim("SuperUser"));
                options.AddPolicy("SuperUserOnly", policy => policy.RequireClaim("SuperUser"));
            }
            );

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("HumanResourceOrHigher", policy => policy.RequireRole("HumanResource", "SuperUser"));
                options.AddPolicy("SuperUserOnly", policy => policy.RequireRole("SuperUser"));
            });
        }

        public static void ConfigureAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                    options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
                        };
                    }
                );
        }

        public static void ConfigureSwaggerGen(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
        }

        public static void ConfigureCors(WebApplicationBuilder builder, string allowedOrigins)
        {

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: allowedOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("localhost:5500",
                                                          "localhost:5500");
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyOrigin();
                                  });
            });
        }

        public static void ConfigureNewtonsoftJson(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }


        public static void ConfigureIdentity(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void ConfigureDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString:DefaultConnection")));
        }

        public static void Configure(WebApplicationBuilder builder)
        {

        }

    }
}
