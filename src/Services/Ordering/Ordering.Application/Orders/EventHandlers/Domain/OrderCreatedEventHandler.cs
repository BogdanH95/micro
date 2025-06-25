using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(
    IPublishEndpoint endpoint,
    IFeatureManager  featureManager,
    ILogger<OrderCreatedEventHandler> logger) 
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType());

        if (await featureManager.IsEnabledAsync("OrderFulfillment", cancellationToken))
        {
            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
            await endpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}