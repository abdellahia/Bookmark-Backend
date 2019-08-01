using System;
using launchpad_backend.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace launchpad_backend.Models
{
    public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
        {
        }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SystemTiles> SystemTiles { get; set; }
        public virtual DbSet<UserTiles> UserTiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Server={InputVariableHelper.GetServernode()};Database={InputVariableHelper.GetDatabase()};User Id={InputVariableHelper.GetUid()}; Password={InputVariableHelper.GetPwd()};");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<SystemTiles>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("SYSTEM_TILES");

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(200);

                entity.Property(e => e.KeycloakClient)
                    .HasColumnName("KEYCLOAK_CLIENT")
                    .HasMaxLength(80);

                entity.Property(e => e.Link).HasColumnName("LINK");

                entity.Property(e => e.Tags)
                    .HasColumnName("TAGS")
                    .HasMaxLength(80);

                entity.Property(e => e.Titel)
                    .HasColumnName("TITEL")
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<UserTiles>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("USER_TILES");

                entity.Property(e => e.Guid)
                    .HasColumnName("GUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(200);

                entity.Property(e => e.Link).HasColumnName("LINK");

                entity.Property(e => e.Tags)
                    .HasColumnName("TAGS")
                    .HasMaxLength(80);

                entity.Property(e => e.Titel)
                    .HasColumnName("TITEL")
                    .HasMaxLength(80);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(80);
            });
        }
    }
}
