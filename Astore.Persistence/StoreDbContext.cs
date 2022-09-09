using Astore.Domain;
using Astore.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Astore.Persistence
{
    public class StoreDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleConfiguration());
            builder.ApplyConfiguration(new CartItemConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new UserProfileConfiguration());

            base.OnModelCreating(builder);
        }
    }
}