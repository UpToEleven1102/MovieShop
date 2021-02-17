using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieShop.Core.Entities;

namespace MovieShop.Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public MovieShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Cast> Casts { get; set; }
        
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>("UserRole",
                    u => u.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId"));
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<Movie>().HasMany(m => m.Casts).WithMany(c => c.Movies)
                .UsingEntity<MovieCast>(
                    m => m.HasOne<Cast>().WithMany().HasForeignKey("CastId"),
                    c => c.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                    b =>
                    {
                        b.Property(mc => mc.Character).HasMaxLength(450);
                        b.HasKey(mc => new {mc.CastId, mc.MovieId, mc.Character});
                    });
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FirstName).HasMaxLength(128);
            builder.Property(t => t.LastName).HasMaxLength(128);
            builder.Property(t => t.Email).HasMaxLength(256);
            builder.Property(t => t.HashedPassword).HasMaxLength(1024);
            builder.Property(t => t.Salt).HasMaxLength(1024);
            builder.Property(t => t.PhoneNumber).HasMaxLength(16);
            builder.Property(t => t.TwoFactorEnabled).HasDefaultValue(false);
            builder.Property(t => t.LockoutEndDate).HasDefaultValue(null);
            builder.Property(t => t.LastLoginDateTime).HasDefaultValue(null);
            builder.Property(t => t.IsLocked).HasDefaultValue(false);
            builder.Property(t => t.AccessFailedCount).HasDefaultValue(0);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.PurchaseNumber).ValueGeneratedOnAdd();
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)").IsRequired();
            builder.Property(p => p.PurchaseDateTime).HasDefaultValueSql("getdate()");
        }
        
        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.Property(r => r.Rating).HasColumnType("decimal(3,2)");
        }
    }
}