using CRUDCleanArchitecture.Application.Common.Interfaces;
using CRUDCleanArchitecture.Infrastructure.Identity;
using CRUDCleanArchitecture.Infrastructure.Persistence;
using CRUDCleanArchitecture.Infrastructure.Persistence.Dapper;
using CRUDCleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Infrastructure.Services.Permission;
using CRUDCleanArchitecture.Infrastructure.Services.Elastic;
using CRUDCleanArchitecture.Infrastructure.Services.Kafka;
using Confluent.Kafka;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(
                  configuration.GetConnectionString("EFCoreDatabase"),
                  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddSingleton<IElasticService, ElasticService>(provider =>
        {
            var elasticsearchUri = new Uri(configuration.GetConnectionString("ElasticServer"));
            return new ElasticService(elasticsearchUri);
        });

        services.AddSingleton<IKafkaService, KafkaService>(provider =>
        {
            var producerConfig = new ProducerConfig() { BootstrapServers = configuration.GetConnectionString("KafkaServer") };
            return new KafkaService(producerConfig);
        });

        services.AddPersistenceDapper(configuration);

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IPermissionService, PermissionService>();

        services.AddRepositories();

        //services.AddIdentityServer()
        //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        //services.AddAuthentication()
        //    .AddIdentityServerJwt();

        //services.AddAuthorization(options =>
        //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));


        //services.AddAuthorizationPolicies(); TODO Agregar policies llegado al caso

        return services;
    }
}
