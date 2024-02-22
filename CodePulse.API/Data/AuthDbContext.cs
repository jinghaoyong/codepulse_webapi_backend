using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "34854d03-dbee-4a50-b2dd-e01cf78cbf43";
            var writeRoleId = "10fb9d4b-a17e-4768-9d96-24ca67e8d0d2";

            //create reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writeRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writeRoleId
                }
            };

            //seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //create an admin user
            var adminUserId = "b33ec477-8b71-43e1-9615-01d0dcfe12b4";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");


            builder.Entity<IdentityUser>().HasData(admin);

            // give roles to admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                    new()
                    {
                     UserId = adminUserId,
                     RoleId = readerRoleId
                    },
                    new()
                    {
                     UserId = adminUserId,
                     RoleId = writeRoleId
                    }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}

