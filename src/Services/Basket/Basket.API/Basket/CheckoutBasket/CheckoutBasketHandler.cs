using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout) 
    : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x=> x.BasketCheckout).NotNull().WithMessage("BasketCheckoutDto is required");
        RuleFor(x=> x.BasketCheckout.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CheckoutBaskedCommandHandler
    (IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(command.BasketCheckout.UserName, cancellationToken);

        var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutEvent>();
        
        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(basket.Username, cancellationToken);
        
        return new CheckoutBasketResult(true);
    }
}