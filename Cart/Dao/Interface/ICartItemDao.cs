using CartDaoApp.Entities;

namespace CartDaoApp.Dao.Interface;

public interface ICartItemDao
{
    List<CartItem> GetAllCartItemsByCartId(Guid cartId);
    void CreateCartItem(CartItem cartItem);
    void RemoveCartItem(Guid cartItemId);
    void ChangeQuantityByItemId(Guid cartItemId, uint quantity);
}