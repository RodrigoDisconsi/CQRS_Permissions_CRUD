using Controllers;
using Microsoft.AspNetCore.Mvc;
using CRUDCleanArchitecture.Application.Permisos.Commands.RequestPermission;
using CRUDCleanArchitecture.Application.Common.Wrapper;
using CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions;
using CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions.Response;
using CRUDCleanArchitecture.Application.Permisos.Commands.ModifyPermission;

namespace API.Controllers;

public class PermisosController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPermissionResponse))]
    public async Task<ActionResult<GetPermissionResponse>> GetPermissions([FromQuery] GetPermissionsQuery request)
    {
        return await Mediator.Send(request);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RequestPermissionResponse))]
    public async Task<ActionResult<Response<RequestPermissionResponse>>> CreatePermissions(RequestPermissionCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ModifyPermissionResponse))]
    public async Task<ActionResult<Response>> Update(ModifyPermissionCommand command)
    {
        return await Mediator.Send(command);
    }
}
