namespace CRUDCleanArchitecture.Domain.Events;
public class PermissionCreatedEvent : BaseEvent
{
    public PermissionCreatedEvent(Permiso permiso)
    {
        Permiso = permiso;
    }

    public Permiso Permiso { get; }
}
