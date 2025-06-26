
namespace Micro.Web.Services;

public interface IOrderingService
{
    [Get("/ordering-service/orders?pageIndex={pageIndex}&pageSize={pageSize}")]
    Task<GetOrdersResponse> GetOrders(int? pageIndex = 0, int? pageSize = 10);

    [Get("/ordering-service/order/{orderName}")]
    Task<GetOrdersByNameResponse> GetOrdersByName(string orderName);
    
    [Get("/ordering-service/order/customer/{customerId}")]
    Task<GetOrdersByCustomerResponse> GetOrdersByCustomer(Guid customerId);
}