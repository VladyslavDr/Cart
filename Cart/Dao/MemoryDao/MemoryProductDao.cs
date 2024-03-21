using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;

namespace CartDaoApp.Dao.MemoryDao;

public class MemoryProductDao : IProductDao
{
    private readonly Dictionary<Guid, Product> _products = [];

    public Product? GetProductById(Guid id) => _products[id];

    public void CreateProduct(Product product) => _products.Add(product.Id, product);
}