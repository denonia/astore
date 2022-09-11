using Astore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astore.Persistence.EntityTypeConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(review => review.Id);
        builder.HasIndex(review => review.Id).IsUnique();
        builder.Property(review => review.Body).IsRequired().HasMaxLength(1000);
        builder.Property(review => review.Rating).IsRequired();

        builder.HasOne(review => review.Author).WithMany(author => author.Reviews);
    }
}