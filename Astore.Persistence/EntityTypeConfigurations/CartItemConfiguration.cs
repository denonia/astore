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
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(item => item.Id);
            builder.HasIndex(item => item.Id).IsUnique();
            builder.Property(item => item.Quantity).HasDefaultValue(1);

            builder.HasOne(item => item.Article).WithMany(article => article.CartItems);
            builder.HasOne(item => item.UserProfile).WithMany(profile => profile.CartItems);
        }
    }
}
