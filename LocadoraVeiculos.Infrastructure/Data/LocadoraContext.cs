using LocadoraVeiculos.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraVeiculos.Infrastructure.Data
{
    public class LocadoraContext(DbContextOptions<LocadoraContext> options) : IdentityDbContext<User>(options)
    {

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<VeiculoAlocado> VeiculosAlocados { get; set; }
        public DbSet<CategoriaVeiculo> CategoriasVeiculo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            User adminUser = new User
            {
                Id = "59306041-3580-4963-8a3c-36a536f9c2d1",
                UserName = "admin@locadora.com",
                NormalizedUserName = "ADMIN@LOCADORA.COM",
                Email = "admin@locadora.com",
                NormalizedEmail = "ADMIN@LOCADORA.COM",
                EmailConfirmed = true,
                SecurityStamp = "78377758-1936-4C5D-9150-70F071253C4D",
                ConcurrencyStamp = "B2C5C9E5-0E2E-4D71-B534-1F677E57C361"
            };
            
            var hasher = new PasswordHasher<User>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin_Acesso_2026!");

            builder.Entity<User>().ToTable("Funcionarios");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("FuncionariosRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("FuncionariosClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("FuncionariosLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("FuncionariosTokens");
            
            builder.Entity<User>().HasData(adminUser);

            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
                entity.Property(u => u.NormalizedEmail).HasMaxLength(256);

                entity.Property(u => u.UserName).IsRequired().HasMaxLength(256);
                entity.Property(u => u.NormalizedUserName).HasMaxLength(256);

                entity.Property(u => u.PhoneNumber).HasMaxLength(1);

                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(512);
                entity.Property(u => u.SecurityStamp).HasMaxLength(256);
                entity.Property(u => u.ConcurrencyStamp).HasMaxLength(256);
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(r => r.Name).IsRequired().HasMaxLength(256);
                entity.Property(r => r.NormalizedName).HasMaxLength(256);
            });

            builder.Entity<Cliente>().HasIndex(c => c.CPF).IsUnique();
            builder.Entity<Cliente>().HasIndex(c => c.Email).IsUnique();

            builder.Entity<CategoriaVeiculo>().HasIndex(c => c.Nome).IsUnique();

        }
    }
}
