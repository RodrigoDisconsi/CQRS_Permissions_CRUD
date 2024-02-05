namespace CRUDCleanArchitecture.Domain.Events;
public class GetPermissionsEvent : BaseEvent
{
    public GetPermissionsEvent(IEnumerable<Permiso> permisos)
    {
        Permisos = permisos;
    }

    public IEnumerable<Permiso> Permisos { get; }
}
