using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories.Entities;

namespace Repositories
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:DefaultConnection"];

            return strConn;
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseSqlServer(GetConnectionString());


        protected override void OnModelCreating(ModelBuilder builder)
        {

            ConfigureRelations(builder);

            base.OnModelCreating(builder);
            ConfigureRoles(builder);
        }


        private void ConfigureRelations(ModelBuilder builder)
        {
            builder.Entity<Employee>(x => x.HasKey(p => new { p.DepartmentId, p.AppUserId }));
            builder.Entity<Employee>().HasOne(f => f.AppUser).WithOne(f => f.Employee).HasForeignKey<AppUser>();
            builder.Entity<Employee>().HasOne(f => f.Department).WithMany(f => f.Employees).HasForeignKey(f => f.Id);

        }

        private void ConfigureRoles(ModelBuilder builder)
        {
            List<IdentityRole> roles = new()
            {
                new IdentityRole()
                {
                    Name = "SuperUser",
                    NormalizedName = "SUPERUSER"
                },

                new IdentityRole()
                {
                    Name = "HumanResource",
                    NormalizedName = "HUMANRESOURCE"
                },
                new IdentityRole()
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },


            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
