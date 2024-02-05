using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using CRUDCleanArchitecture.Infrastructure.Persistence;
using API.Services;
using API.Filters;
using CRUDCleanArchitecture.Application.Common.Interfaces.Services;

namespace API;
public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var corsAllowedOrigins = "AllowSpecificOrigins";
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        });
        services.AddCors(options =>
        {
            options.AddPolicy(name: corsAllowedOrigins,
                              builder =>
                              {
                                  var origins = configuration.GetValue<string>("CorsOrigins").Split(",");
                                  builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
                              });
        });
        services.AddFluentValidationAutoValidation();
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddHttpContextAccessor();
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        return services;
    }
}
