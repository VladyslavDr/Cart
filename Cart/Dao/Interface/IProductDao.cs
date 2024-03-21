using CartDaoApp.Entities;

namespace CartDaoApp.Dao.Interface
{
    public interface IProductDao
    {
        Product? GetProductById(Guid id);
        void CreateProduct(Product product);
    }
}