using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Medalla> Medallas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de Categoria
            builder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(e => e.CreadoUtc)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relación con ApplicationUser
                entity.HasOne(e => e.CreadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(e => e.CreadoPorUsuarioId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);

                // Índices
                entity.HasIndex(e => e.Nombre).IsUnique();
                entity.HasIndex(e => e.CreadoPorUsuarioId);
            });

            //Configuración de Ingredientes
            builder.Entity<Ingrediente>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.Unidad_medida)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Gr");

                entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(2000);

                entity.Property(e => e.CreadoUtc)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(e => e.Nombre).IsUnique();
            });

            //configuración de Recetas
            builder.Entity<Receta>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.Instrucciones)
                .IsRequired()
                .HasMaxLength(10000);

                entity.Property(e => e.CreadoUtc)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

                //relaciones

                //entity.HasOne(e => e.paraTiempo)
                //.WithMany()
                //.HasForeignKey(e => e.paraTiempoId)
                //.OnDelete(DeleteBehavior.SetNull)
                //.IsRequired();

                entity.HasOne(e => e.creadoPorUsuario)
                .WithMany()
                .HasForeignKey(e => e.creadoPorUsuarioId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();

                //indices
                //entity.HasIndex(e => e.creadoPorUsuarioId);
                //entity.HasIndex(e => e.paraTiempoId);
                //entity.HasIndex(e => e.Ingredientes);
                ////entity.HasIndex(e => e.Categorias);
            });

            builder.Entity<Medalla>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.nombre)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.descripcion)
                .IsRequired()
                .HasMaxLength(500);

                entity.Property(e => e.icono)
                .IsRequired()
                .HasMaxLength(500);

                entity.Property(e => e.tipoReq)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.cantidadReq)
                .IsRequired();

                entity.Property(e => e.habilitada)
                .IsRequired();

                entity.Property(e => e.CreadoUtc)
                .IsRequired(true)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
