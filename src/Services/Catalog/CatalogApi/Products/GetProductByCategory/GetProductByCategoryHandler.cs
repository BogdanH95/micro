using Marten.Linq.QueryHandlers;

namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session
                .Query<Product>()
                .Where(x =>
                    x.Categories.Any(c =>
                        string.Equals(c, query.Category, StringComparison.OrdinalIgnoreCase)
                    )
                )
                .ToListAsync(cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}
