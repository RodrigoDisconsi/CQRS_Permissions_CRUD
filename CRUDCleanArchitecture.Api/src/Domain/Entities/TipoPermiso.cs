namespace CRUDCleanArchitecture.Domain.Entities;
public class TipoPermiso : HasDomainEvent
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public virtual ICollection<Permiso> Permisos { get; set; }
}
