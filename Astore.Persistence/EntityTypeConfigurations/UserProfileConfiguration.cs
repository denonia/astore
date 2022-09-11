using Astore.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astore.Persistence.EntityTypeConfigurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(profile => profile.UserId);
        builder.Property(profile => profile.Address);
        builder.Property(profile => profile.FirstName);
        builder.Property(profile => profile.LastName);

        builder.HasOne<IdentityUser>()
            .WithOne()
            .HasPrincipalKey<UserProfile>(x => x.UserId);
    }
}