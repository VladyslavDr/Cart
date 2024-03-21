using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;
using System.Text.Json;

namespace CartDaoApp.Dao.FileDao;

public class FileProductDao : IProductDao
{
    private const string DirectoryPath = "Products";
    public Product? GetProductById(Guid id)
    {
        string filePath = $@"{DirectoryPath}\{id}.json";

        if (File.Exists(filePath))
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ProductConverter());

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Product>(json, options);
        }

        return null;
    }

    public void CreateProduct(Product product)
    {
        var filePath = Path.Combine(DirectoryPath, $"{product.Id}.json");
        var json = JsonSerializer.Serialize(product);

        File.WriteAllText(filePath, json);
    }
}