using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Extensions;

public static class InitialData
{
    public static IEnumerable<Customer> Customers =>
    [
        Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "John", "john@acme.boom"),
        Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "Silvia", "Silvia@acme.boom")
    ];

    public static IEnumerable<Product> Products =>
    [
        Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "IPhone X", 500),
        Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung 10", 400),
        Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Huawei Plus", 650),
        Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Xiaomi Mi", 450)
    ];

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("John", "Smith", "John@acme.boom", "bla bla acme boom"
                , "ACME", "STUDIO", "123");
            var address2 = Address.Of("Silvia", "Smith", "Silvia@acme.boom", "bla bla acme boom"
                , "ACME", "STUDIO", "123");

            var payment1 = Payment.Of("John Smith", "1234-1234-1234-1234"
                , "12/39", "123", 1);
            var payment2 = Payment.Of("Silvia Smith", "3234-1234-1234-1234"
                , "02/39", "223", 1);

            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid())
                , CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa"))
                , OrderName.Of("ORD_1")
                , address1
                , address1
                , payment1
                , OrderStatus.Draft);
            order1.Add(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 1, 500);
            order1.Add(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 1, 400);

            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid())
                , CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"))
                , OrderName.Of("ORD_2")
                , address1
                , address1
                , payment1
                , OrderStatus.Draft);
            order2.Add(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 1, 650);
            order2.Add(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 1, 450);

            return [order1, order2];
        }
    }
}