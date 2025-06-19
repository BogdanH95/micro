namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger logger) 
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType());
        return Task.CompletedTask;
    }
}