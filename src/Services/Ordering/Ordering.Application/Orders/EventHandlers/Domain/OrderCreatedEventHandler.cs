using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint endpoint, ILogger<OrderCreatedEventHandler> logger) 
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType());

        var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
        endpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        
        return Task.CompletedTask;
    }
}