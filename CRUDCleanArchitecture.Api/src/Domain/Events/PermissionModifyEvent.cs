namespace CRUDCleanArchitecture.Domain.Events;
public class PermissionModifyEvent : BaseEvent
{
    public PermissionModifyEvent(Permiso permiso)
    {
        Permiso = permiso;
    }

    public Permiso Permiso { get; }
}
