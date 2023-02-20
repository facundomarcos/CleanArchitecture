

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c028929a-a55e-45ce-99bf-1123ed874a4b",
                    UserId= "47650191-bb6d-4b41-89d9-dd6c1b6761ca"
                },
                 new IdentityUserRole<string>
                 {
                     RoleId = "667e6e44-9b37-4370-99fc-25c6cd845ac5",
                     UserId = "1f45dd74-d849-49d5-aff3-5c6486378b7f"
                 }
                );
        }
    }
}
