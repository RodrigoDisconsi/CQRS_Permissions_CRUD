using AutoMapper;
using CRUDCleanArchitecture.Application.Common.Mappings;
using CRUDCleanArchitecture.Domain.Entities;
using CRUDCleanArchitecture.Domain.Queries.Permissions;

namespace CRUDCleanArchitecture.Application.Permisos.Queries.GetPermissions.Response;
public class PermissionDto : IMapFrom<PermissionView>
{
    public int PermisoId { get; set; }
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public DateTime FechaPermiso { get; set; }
    public PermissionTypeDto TipoPermiso { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PermissionView, PermissionDto>()
               .ForMember(dst => dst.TipoPermiso, opt => opt.MapFrom(x => new PermissionTypeDto { TipoPermisoId = x.TipoPermisoId, Descripcion = x.Descripcion}));

        profile.CreateMap<PermissionDto, Permiso>()
                .ForMember(dst => dst.TipoPermisoId, opt => opt.MapFrom(x => x.TipoPermiso.TipoPermisoId))
                .ForMember(dst => dst.TipoPermiso, opt => opt.MapFrom(x => new TipoPermiso { Id = x.TipoPermiso.TipoPermisoId, Descripcion = x.TipoPermiso.Descripcion }));
    }
}

public class PermissionTypeDto
{
    public int TipoPermisoId { get; set; }
    public string Descripcion { get; set; }
}
