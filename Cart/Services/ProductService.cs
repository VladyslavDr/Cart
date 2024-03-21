using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;

namespace CartDaoApp.Services;

public class ProductService(IProductDao productDao)
{
    public Product GetProductById(Guid id) => productDao.GetProductById(id);

    public void AddProduct(Product product) => productDao.CreateProduct(product);
}