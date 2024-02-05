using FluentValidation;

namespace CRUDCleanArchitecture.Application.Permisos.Commands.RequestPermission;
public class RequestPermissionValidator : AbstractValidator<RequestPermissionCommand>
{
    public RequestPermissionValidator()
    {
        RuleFor(x => x.NombreEmpleado)
            .NotEmpty()
            .WithMessage("Nombre de} empleado requerido.");
        
        RuleFor(x => x.ApellidoEmpleado)
            .NotEmpty()
            .WithMessage("Nombre de empleado requerido.");

        RuleFor(x => x.TipoPermisoId)
            .NotEmpty()
            .WithMessage("Tipo de permiso es requerido.");

        RuleFor(x => x.FechaPermiso)
            .NotEmpty()
            .WithMessage("Fecha de permiso es requerido.");
    }
}
