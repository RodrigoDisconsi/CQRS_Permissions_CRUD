using AutoMapper;
using CRUDCleanArchitecture.Application.Common.Exceptions;
using CRUDCleanArchitecture.Application.Common.Interfaces;
using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Application.Common.Wrapper;
using CRUDCleanArchitecture.Application.Permisos.Commands.ModifyPermission;
using CRUDCleanArchitecture.Application.Permisos.Commands.RequestPermission;
using CRUDCleanArchitecture.Domain.Entities;
using CRUDCleanArchitecture.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CRUDCleanArchitecture.Infrastructure.Services.Permission;
public class PermissionService : IPermissionService
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<PermissionService> _logger;
    private readonly MapperConfiguration _mapperConfigurator;
    private readonly IMapper _mapper;

    public PermissionService(IApplicationDbContext context, ILogger<PermissionService> logger)
    {
        _context = context;
        _logger = logger;
        _mapperConfigurator = InitMapperConfigurator();
        _mapper = _mapperConfigurator.CreateMapper();
    }

    public async Task<Response<RequestPermissionResponse>> RequestPermission(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Se inicia la creación de permiso");
            var permission = _mapper.Map<Permiso>(request);

            permission.AddDomainEvent(new PermissionCreatedEvent(permission));

            await _context.Permisos.AddAsync(permission);

            await _context.SaveChangesAsync(cancellationToken);

            return await Response<RequestPermissionResponse>.SuccessAsync(new RequestPermissionResponse() { PermisoId = permission.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear permiso: {ex.Message} - {ex.StackTrace}");
            return await Response<RequestPermissionResponse>.FailAsync("Error inesperado al crear permisos");
        }
    }

    public async Task<Response> UpdatePermission(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Se inicia la creación de permiso");
            var permission = await _context.Permisos.FirstOrDefaultAsync(p => p.Id == request.PermissionId);
            
            if (permission == null) throw new NotFoundException(nameof(Permiso), request.PermissionId);

            permission.AddDomainEvent(new PermissionModifyEvent(permission));

            permission.NombreEmpleado = request.NombreEmpleado;
            permission.ApellidoEmpleado = request.ApellidoEmpleado;
            permission.FechaPermiso = request.FechaPermiso;
            permission.TipoPermisoId = request.TipoPermisoId;

            await _context.SaveChangesAsync(cancellationToken);

            return await Response<ModifyPermissionResponse>.SuccessAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al modificar permiso: {ex.Message} - {ex.StackTrace}");
            return await Response<ModifyPermissionResponse>.FailAsync("Error inesperado al crear permisos");
        }
    }

    private MapperConfiguration InitMapperConfigurator()
    {
        return
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RequestPermissionCommand, Permiso>();
            });

    }
}
