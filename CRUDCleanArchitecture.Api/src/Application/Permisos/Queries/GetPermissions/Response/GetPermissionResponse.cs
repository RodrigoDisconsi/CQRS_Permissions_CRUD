using CRUDCleanArchitecture.Application.Common.Models;

namespace CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions.Response;
public class GetPermissionResponse
{
    public PaginatedList<PermissionDto> Permisos {  get; set; }
}
