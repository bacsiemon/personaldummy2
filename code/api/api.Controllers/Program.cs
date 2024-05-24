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


            ServiceConfig.ConfigureDependencyInjection(builder);

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
