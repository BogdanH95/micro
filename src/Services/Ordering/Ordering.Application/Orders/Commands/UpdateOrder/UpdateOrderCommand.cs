namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool isSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x=> x.Order.Id).NotEmpty().WithMessage("Order id is required");
        RuleFor(x=> x.Order.OrderName).NotEmpty().WithMessage("OrderName is required");
        RuleFor(x=> x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
    }
}