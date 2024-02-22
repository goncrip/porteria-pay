using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess
{
    public partial class PorteriaContext : DbContext
    {
        public PorteriaContext()
        {
        }

        public PorteriaContext(DbContextOptions<PorteriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Ingreso> Ingresos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<TipoCarga> TipoCargas { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:porteria-paysandu.database.windows.net,1433;Database=Porteria;User Id=cristiangonzalez;Password='2{]V0y2QX;|u';Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.ToTable("Empresa");

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Ingreso>(entity =>
            {
                entity.HasKey(e => e.IdIngreso);

                entity.ToTable("Ingreso");

                entity.Property(e => e.FechaEgreso).HasColumnType("datetime");

                entity.Property(e => e.FechaIngreso).HasColumnType("datetime");

                entity.Property(e => e.Peso).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingreso_Empresa");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingreso_Persona");

                entity.HasOne(d => d.IdTipoCargaNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.IdTipoCarga)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingreso_TipoCarga");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Ingresos)
                    .HasForeignKey(d => d.IdVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingreso_Vehiculo");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("Persona");

                entity.Property(e => e.Apellidos).HasMaxLength(100);

                entity.Property(e => e.Celular).HasMaxLength(20);

                entity.Property(e => e.Documento).HasMaxLength(20);

                entity.Property(e => e.Nombres).HasMaxLength(100);

                entity.Property(e => e.Pais).HasMaxLength(2);

                entity.Property(e => e.TipoDocumento).HasMaxLength(3);
            });

            modelBuilder.Entity<TipoCarga>(entity =>
            {
                entity.HasKey(e => e.IdTipoCarga);

                entity.ToTable("TipoCarga");

                entity.Property(e => e.Descripcion).HasMaxLength(50);
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo);

                entity.ToTable("Vehiculo");

                entity.Property(e => e.Matricula).HasMaxLength(20);

                entity.Property(e => e.Pais).HasMaxLength(2);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
