using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "47650191-bb6d-4b41-89d9-dd6c1b6761ca",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    Nombre = "Facundo",
                    Apellidos = "Marcos",
                    UserName = "facundomarcos",
                    NormalizedUserName = "facundomarcos",
                    PasswordHash = hasher.HashPassword(null, "facundo123@"),
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "1f45dd74-d849-49d5-aff3-5c6486378b7f",
                    Email = "user@localhost.com",
                    NormalizedEmail = "user@localhost.com",
                    Nombre = "User",
                    Apellidos = "UserLast",
                    UserName = "userlast",
                    NormalizedUserName = "userlast",
                    PasswordHash = hasher.HashPassword(null, "facundo123@"),
                    EmailConfirmed = true,


                }
                );
        }
    }
}


//https://www.guidgenerator.com/