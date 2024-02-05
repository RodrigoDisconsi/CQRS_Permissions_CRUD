﻿using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CRUDCleanArchitecture.Application.Permisos.EventHandlers;
public class PermissionCreatedEventHandler : INotificationHandler<PermissionCreatedEvent>
{
    private readonly IKafkaService _kafkaService;
    private readonly IElasticService _elasticsearchService;
    private readonly ILogger<PermissionCreatedEventHandler> _logger;

    public PermissionCreatedEventHandler(IKafkaService kafkaService, IElasticService elasticsearchService, ILogger<PermissionCreatedEventHandler> logger)
    {
        _kafkaService = kafkaService;
        _elasticsearchService = elasticsearchService;
        _logger = logger;
    }

    public async Task Handle(PermissionCreatedEvent notification, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

            await _kafkaService.SendMessageAsync("request");
            _elasticsearchService.IndexDocument(notification.Permiso);

        }
        catch(Exception ex)
        {
            _logger.LogError($"Error Domain Event: {notification.GetType().Name}, Ex: {ex.Message}");
        }
    }
}
