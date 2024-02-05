using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace CRUDCleanArchitecture.Infrastructure.Persistence.Dapper;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDapper(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DapperDatabase");

        services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));

        return services;
    }
}
