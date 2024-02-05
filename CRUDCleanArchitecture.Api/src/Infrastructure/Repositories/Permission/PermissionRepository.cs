using CRUDCleanArchitecture.Application.Common.Interfaces.Repositories;
using CRUDCleanArchitecture.Application.Common.Models;
using CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions;
using CRUDCleanArchitecture.Domain.Queries;
using CRUDCleanArchitecture.Domain.Queries.Permissions;
using CRUDCleanArchitecture.Infrastructure.Extensions;
using Dapper;
using System.Data;

namespace CRUDCleanArchitecture.Infrastructure.Repositories.Permission;
public class PermissionRepository : BaseRepository, IPermissionRepository
{
    private readonly IDbConnection _connection;

    public PermissionRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<PaginatedList<PermissionView>> GetPermissions(GetPermissionsQuery request)
    {
        DynamicParameters parameters = new DynamicParameters();

        var query = @"
        SELECT
            p.Id AS PermisoId,
            p.NombreEmpleado,
            p.ApellidoEmpleado,
            p.FechaPermiso,
            p.TipoPermisoId,
            t.Descripcion
        FROM 
            [dbo].[Permissions] p
        JOIN
            [dbo].[PermissionTypes] t ON p.TipoPermisoId = t.Id";

        query += this.AplicarBusqueda(request.TextoBusqueda, parameters, typeof(PersonaView));
        query += string.IsNullOrWhiteSpace(request.OrderCriteria.Column) ? " ORDER BY ApellidoEmpleado DESC" :
            String.Format(" ORDER BY {0} {1}", request.OrderCriteria.Column,
                request.OrderCriteria.Ascending ? string.Empty : "DESC");

        return await _connection.PaginatedListAsync<PermissionView>(query, parameters, request.PageNumber, request.PageSize);
    }
}

