using CRUDCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUDCleanArchitecture.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Permiso> Permisos { get; set; }
    DbSet<TipoPermiso> TiposPermisos { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
