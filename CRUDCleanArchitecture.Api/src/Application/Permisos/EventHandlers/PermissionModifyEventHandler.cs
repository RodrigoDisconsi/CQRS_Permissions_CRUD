using CRUDCleanArchitecture.Application.Common.Interfaces.Services;
using CRUDCleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CRUDCleanArchitecture.Application.Permisos.EventHandlers;
public class PermissionModifyEventHandler : INotificationHandler<PermissionModifyEvent>
{
    private readonly IKafkaService _kafkaService;
    private readonly ILogger<PermissionModifyEventHandler> _logger;

    public PermissionModifyEventHandler(IKafkaService kafkaService, ILogger<PermissionModifyEventHandler> logger)
    {
        _kafkaService = kafkaService;
        _logger = logger;
    }

    public async Task Handle(PermissionModifyEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CRUDCleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        await _kafkaService.SendMessageAsync("modify");
    }
}
