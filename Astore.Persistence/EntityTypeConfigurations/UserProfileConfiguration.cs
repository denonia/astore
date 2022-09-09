using Astore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Astore.Persistence.EntityTypeConfigurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.Property(profile => profile.Address).IsRequired();

            builder.HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<UserProfile>(x => x.UserId);
        }
    }
}
