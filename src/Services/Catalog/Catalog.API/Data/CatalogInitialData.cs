using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(token: cancellation))
            return;
        //Marten UPSERT will cater for existing records
        session.Store(GetSeedData());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<object> GetSeedData()
    {
        return [
            new Product {
                Id = new Guid("01971d58-ec54-4522-87d2-b9ff84a1e922"),
                Name = "Iphone X",
                Description = "This phone is the company's biggest change to its feature and screen.",
                Categories = ["smart phone", "apple"],
                Price = 950.00M,
                ImageFile = "iphone-x.png"
            },new Product {
                Id = new Guid("01971d66-44ef-45b5-8362-89bd79dfb5ce"),
                Name = "Samsung 10",
                Description = "This phone is the company's biggest change to its feature and screen.",
                Categories = ["smart phone", "samsung"],
                Price = 840.00M,
                ImageFile = "samsung.png"
            },new Product {
                Id = new Guid("01971d66-5ed9-481a-8feb-afeaea7e1e1a"),
                Name = "Iphone 16 Pro Max",
                Description = "This phone is the newer and better than any other.",
                Categories = ["smart phone", "apple"],
                Price = 1250.00M,
                ImageFile = "iphone-16.png"
            },new Product {
                Id = new Guid("01971d66-7299-4304-8f52-64d0d5cb235c"),
                Name = "Asus ROG",
                Description = "This is laptop",
                Categories = ["computers", "laptop", "asus"],
                Price = 1450.00M,
                ImageFile = "asus-rog.png"
            },new Product {
                Id = new Guid("01971d66-9143-4d78-90fb-b6e435d19fbf"),
                Name = "Macbook Pro ",
                Description = "This laptop is top of the line apple laptop .",
                Categories = ["laptop", "apple"],
                Price = 2500.00M,
                ImageFile = "macbook-pro.png"
            },new Product {
                Id = new Guid("01971d66-b192-4cf4-865c-5491d30a2c4d"),
                Name = "Gaming Desktop 3dxas",
                Description = "Gaming desktop to play PUBG (no fortnite) ",
                Categories = ["computers", "gaming"],
                Price = 1699.00M,
                ImageFile = "gaming-desktop.png"
            },new Product {
                Id = new Guid("01971d66-c10f-45c9-b502-2edcbeb699ba"),
                Name = "Ultra Mouse",
                Description = "Excelent mouse for FPS games.",
                Categories = ["mouse", "gaming"],
                Price = 280.00M,
                ImageFile = "ultra-mouse.png"
            },
        ];
    }
}