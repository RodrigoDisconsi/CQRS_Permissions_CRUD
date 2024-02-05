using AutoMapper;
using CRUDCleanArchitecture.Application.Common.Base;
using CRUDCleanArchitecture.Application.Common.Interfaces.Repositories;
using MediatR;
using CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions.Response;
using CRUDCleanArchitecture.Domain.Events;
using CRUDCleanArchitecture.Domain.Entities;

namespace CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions;
public class GetPermissionsQuery : PagerBase, IRequest<GetPermissionResponse>
{
    public string? TextoBusqueda { get; set; }
}

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, GetPermissionResponse>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetPermissionsQueryHandler(IPermissionRepository permissionRepository, IMapper mapper, IMediator mediator)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<GetPermissionResponse> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var response = new GetPermissionResponse
        {
            Permisos = (await _permissionRepository.GetPermissions(request)).Map<PermissionDto>(_mapper.ConfigurationProvider)
        };
        await _mediator.Publish(new GetPermissionsEvent(response.Permisos.Map<Permiso>(_mapper.ConfigurationProvider).Items));

        return await Task.FromResult(response);
    }
}

