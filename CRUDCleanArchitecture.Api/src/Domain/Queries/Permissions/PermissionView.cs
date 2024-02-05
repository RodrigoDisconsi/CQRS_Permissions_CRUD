using CRUDCleanArchitecture.Domain.Attributes;

namespace CRUDCleanArchitecture.Domain.Queries.Permissions;
public class PermissionView
{
    public int PermisoId { get; set; }
    [Buscador]
    public string NombreEmpleado { get; set; }
    [Buscador]
    public string ApellidoEmpleado { get; set; }
    public DateTime FechaPermiso { get; set; }
    public int TipoPermisoId { get; set; }
    public string Descripcion { get; set; }
}
