using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;

namespace CartDaoApp.Dao.MemoryDao;

public class MemoryCartDao : ICartDao
{
    private readonly Dictionary<Guid, Cart> _carts = [];

    public Cart? GetCartByUserId(Guid id) => _carts.Values.FirstOrDefault(cart => cart.UserId == id);

    public void CreateCartByUserId(Guid userId)
    {
        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            UserId = userId
        };

        _carts.Add(cart.Id, cart);
    }

    public void UpdateTotalPrice(Guid cartId, decimal newTotalPrice)
    {
        _carts[cartId].TotalPrice = newTotalPrice;
    }
}