using Astore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astore.Persistence.EntityTypeConfigurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(article => article.Id);
            builder.HasIndex(article => article.Id).IsUnique();
            builder.Property(article => article.Name).HasMaxLength(250).IsRequired();
            builder.Property(article => article.Price).IsRequired();

            builder.HasOne(article => article.Category)
                .WithMany(category => category.Articles);
            builder.HasMany(article => article.Reviews)
                .WithOne(review => review.Article);
            builder.HasMany(article => article.FavoritedBy)
                .WithMany(userProfile => userProfile.Favorites);
        }
    }
}
