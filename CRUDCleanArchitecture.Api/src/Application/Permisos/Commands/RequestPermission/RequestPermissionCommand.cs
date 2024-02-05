using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Application.Common.Wrapper;
using MediatR;

namespace CRUDCleanArchitecture.Application.Permisos.Commands.RequestPermission;
public class RequestPermissionCommand : IRequest<Response<RequestPermissionResponse>>
{
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public int TipoPermisoId { get; set; }
    public DateTime FechaPermiso { get; set; }
}

public class RequestPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, Response<RequestPermissionResponse>>
{
    private readonly IPermissionService _permissionService;

    public RequestPermissionCommandHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    public async Task<Response<RequestPermissionResponse>> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionService.RequestPermission(request, cancellationToken);
    }
}