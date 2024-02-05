using CRUDCleanArchitecture.Application.Common.Wrapper;
using CRUDCleanArchitecture.Application.Permisos.Commands.ModifyPermission;
using CRUDCleanArchitecture.Application.Permisos.Commands.RequestPermission;

namespace CRUDCleanArchitecture.Application.Common.Interfaces.Services;
public interface IPermissionService
{
    Task<Response<RequestPermissionResponse>> RequestPermission(RequestPermissionCommand request, CancellationToken cancellationToken);
    Task<Response> UpdatePermission(ModifyPermissionCommand request, CancellationToken cancellationToken);
}
