using Microsoft.Extensions.DependencyInjection;
using CRUDCleanArchitecture.Application.Common.Interfaces.Repositories;
using CRUDCleanArchitecture.Infrastructure.Repositories.Permission;

namespace CRUDCleanArchitecture.Infrastructure.Repositories;
public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IPermissionRepository, PermissionRepository>();

        return services;
    }
}
