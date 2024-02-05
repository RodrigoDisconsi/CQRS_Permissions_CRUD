using CRUDCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRUDCleanArchitecture.Infrastructure.Persistence.EFConfigurations;
public class TipoPermisoConfiguration : IEntityTypeConfiguration<TipoPermiso>
{
    public void Configure(EntityTypeBuilder<TipoPermiso> builder)
    {
        builder.ToTable("PermissionTypes");

        builder.HasKey(e => e.Id).HasName("PK_PermissionTypes_Id");

        builder.Property(t => t.Descripcion)
            .HasMaxLength(100)
            .IsRequired();
    }
}
