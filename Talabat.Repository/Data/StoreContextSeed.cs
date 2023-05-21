using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedData(StoreContext context)
        {
            if (!context.ProductBrands.Any()) {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands is not null && brands.Count > 0) {
                    foreach (var brand in brands) {
                        await context.Set<ProductBrand>().AddAsync(brand);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (types is not null && types.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await context.Set<ProductType>().AddAsync(type);
                    }
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Talabat.Repository/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await context.Set<Product>().AddAsync(product);
                    }
                    await context.SaveChangesAsync();
                }
            }
        } 
    }
}
