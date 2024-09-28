using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Domain.Entities;

namespace SocialMediaApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : 
        IdentityDbContext<AppUser>(options)
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-One: User <-> UserProfile
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);

            // One-to-Many: User <-> Post
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            // One-to-Many: Post <-> Comment
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);

            // One-to-Many: User <-> Comment
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            // One-to-Many: Post <-> Like
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithOne(l => l.Post)
                .HasForeignKey(l => l.PostId);

            // One-to-Many: User <-> Like
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.Property(p => p.DateOfBirth)
                    .HasColumnType("date")  // Or "date" if you prefer
                    .IsRequired(false);         // Make it optional (nullable)

                // Example for adding a default timestamp if you had a CreatedAt property
                entity.Property(p => p.DateOfBirth)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(c => c.CreatedAt)
                    .HasColumnType("timestamp"); // Or "date" if you prefer

                // Example for adding a default timestamp if you had a CreatedAt property
                entity.Property(c => c.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });


            modelBuilder.Entity<Like>(entity =>
            {
                entity.Property(l => l.CreatedAt)
                    .HasColumnType("timestamp"); // Or "date" if you prefer

                // Example for adding a default timestamp if you had a CreatedAt property
                entity.Property(l => l.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });


            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(p => p.CreatedAt)
                    .HasColumnType("timestamp"); // Or "date" if you prefer

                // Example for adding a default timestamp if you had a CreatedAt property
                entity.Property(p => p.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
