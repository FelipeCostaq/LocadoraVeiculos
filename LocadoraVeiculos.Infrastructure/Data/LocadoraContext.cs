using LocadoraVeiculos.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Infrastructure.Data
{
    public class LocadoraContext(DbContextOptions<LocadoraContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
                entity.Property(u => u.NormalizedEmail).HasMaxLength(256);

                entity.Property(u => u.UserName).IsRequired().HasMaxLength(256);
                entity.Property(u => u.NormalizedUserName).HasMaxLength(256);

                entity.Property(u => u.PhoneNumber).HasMaxLength(20);

                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(512);
                entity.Property(u => u.SecurityStamp).HasMaxLength(256);
                entity.Property(u => u.ConcurrencyStamp).HasMaxLength(256);
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(r => r.Name).IsRequired().HasMaxLength(256);
                entity.Property(r => r.NormalizedName).HasMaxLength(256);
            });

        }
    }
}
