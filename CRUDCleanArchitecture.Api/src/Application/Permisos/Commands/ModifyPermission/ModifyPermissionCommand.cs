using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Application.Common.Wrapper;
using MediatR;

namespace CRUDCleanArchitecture.Application.Permisos.Commands.ModifyPermission;
public class ModifyPermissionCommand : IRequest<Response>
{
    public int PermissionId { get; set; }
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public int TipoPermisoId { get; set; }
    public DateTime FechaPermiso { get; set; }
}

public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, Response>
{
    private readonly IPermissionService _permissionService;

    public ModifyPermissionCommandHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    public async Task<Response> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionService.UpdatePermission(request, cancellationToken);
    }
}