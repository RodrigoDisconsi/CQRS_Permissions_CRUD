namespace CRUDCleanArchitecture.Domain.Entities;
public class Permiso : HasDomainEvent
{
    public int Id { get; set; }
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public DateTime FechaPermiso { get; set; }
    public int TipoPermisoId { get; set; }
    public virtual TipoPermiso TipoPermiso { get; set; }
}
