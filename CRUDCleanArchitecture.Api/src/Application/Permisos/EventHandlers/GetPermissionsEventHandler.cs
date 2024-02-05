using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CRUDCleanArchitecture.Application.Permisos.EventHandlers;
public class GetPermissionsEventHandler : INotificationHandler<GetPermissionsEvent>
{
    private readonly IKafkaService _kafkaService;
    private readonly IElasticService _elasticsearchService;
    private readonly ILogger<GetPermissionsEventHandler> _logger;

    public GetPermissionsEventHandler(IKafkaService kafkaService, ILogger<GetPermissionsEventHandler> logger, IElasticService elasticsearchService)
    {
        _kafkaService = kafkaService;
        _logger = logger;
        _elasticsearchService = elasticsearchService; 
    }

    public async Task Handle(GetPermissionsEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            await _kafkaService.SendMessageAsync("get");
            if (notification.Permisos.Count() > 0) await _elasticsearchService.BulkIndexAsync(notification.Permisos);

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Domain Event: {notification.GetType().Name}, Ex: {ex.Message}");
        }
    }
}
