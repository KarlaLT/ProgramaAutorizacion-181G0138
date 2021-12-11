using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AutorizacionConGalletitas.Models
{
    public partial class rolesContext : DbContext
    {
        public rolesContext()
        {
        }

        public rolesContext(DbContextOptions<rolesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Registro> Registros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=roles", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.21-mysql"));

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4");

            modelBuilder.Entity<Registro>(entity =>
            {
                entity.HasKey(e => e.Usuario)
                    .HasName("PRIMARY");

                entity.ToTable("registros");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(45)
                    .HasColumnName("usuario");

                entity.Property(e => e.Contra)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("contra");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("rol");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
