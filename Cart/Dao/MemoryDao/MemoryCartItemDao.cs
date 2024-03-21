using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;

namespace CartDaoApp.Dao.MemoryDao;

public class MemoryCartItemDao : ICartItemDao
{
    private readonly Dictionary<Guid, CartItem> _cartItems = [];

    public List<CartItem> GetAllCartItemsByCartId(Guid cartId) => _cartItems.Values.Where(cartItem => cartItem.CartId == cartId).ToList();

    public void CreateCartItem(CartItem cartItem)
    {
        _cartItems.Add(cartItem.Id, cartItem);
    }

    public void RemoveCartItem(Guid cartItemId) => _cartItems.Remove(cartItemId);

    public void ChangeQuantityByItemId(Guid cartItemId, uint quantity)
    {
        _cartItems[cartItemId].Quantity = quantity;
    }
}