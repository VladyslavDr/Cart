using CartDaoApp.Entities;

namespace CartDaoApp.Dao.Interface;

public interface ICartDao
{
    Cart? GetCartByUserId(Guid id);
    void CreateCartByUserId(Guid userId);
    void UpdateTotalPrice(Guid cartId, decimal newTotalPrice);
}