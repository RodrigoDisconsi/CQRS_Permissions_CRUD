using CRUDCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRUDCleanArchitecture.Infrastructure.Persistence.EFConfigurations;
public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
{
    public void Configure(EntityTypeBuilder<Permiso> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(e => e.Id).HasName("PK_Permissions_Id");

        builder.Property(t => t.NombreEmpleado)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.ApellidoEmpleado)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.FechaPermiso)
            .HasColumnType("datetime");

        builder.HasOne(c => c.TipoPermiso)
                  .WithMany(t => t.Permisos)
                  .HasForeignKey(c => c.TipoPermisoId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_Contactos_TelefonoTipos_TelefonoTipoId");
    }
}
