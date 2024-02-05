using CRUDCleanArchitecture.Application.Common.Models;
using CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions;
using CRUDCleanArchitecture.Domain.Queries.Permissions;

namespace CRUDCleanArchitecture.Application.Common.Interfaces.Repositories;
public interface IPermissionRepository
{
    Task<PaginatedList<PermissionView>> GetPermissions(GetPermissionsQuery request);
}
