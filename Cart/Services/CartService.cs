using CartDaoApp.Dao.Interface;
using CartDaoApp.Entities;
using Newtonsoft.Json;

namespace CartDaoApp.Services;

public class CartService (ICartDao cartDao, ICartItemDao cartItemDao)
{
    public Cart GetCartByUserId(Guid userId)
    {
        var cart = cartDao.GetCartByUserId(userId);

        if (cart == null)
        {
            cartDao.CreateCartByUserId(userId);
            cart = cartDao.GetCartByUserId(userId);
        }

        return cart!;
    }

    public List<CartItem> GetCartItems(Cart cart) => cartItemDao.GetAllCartItemsByCartId(cart.Id);

    public void ClearCart(Cart cart)
    {
        var cartItems = cartItemDao.GetAllCartItemsByCartId(cart.Id);

        foreach (var cartItem in cartItems)
        {
            cartItemDao.RemoveCartItem(cartItem.Id);
        }
        cartDao.UpdateTotalPrice(cart.Id, 0);
    }

    public void AddProduct(Cart cart, Product product)
    {
        var cartItems = cartItemDao.GetAllCartItemsByCartId(cart.Id);
        var cartItem = cartItems.FirstOrDefault(cartItem => cartItem.ProductId == product.Id);

        if (cartItem == null)
        {
            var newCartItem = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = 1
            };

            cartItemDao.CreateCartItem(newCartItem);
            return;
        }

        cartItemDao.ChangeQuantityByItemId(cartItem.Id, ++cartItem.Quantity);
    }
    
    public void RemoveProduct(Cart cart, Product product)
    {
        var cartItems = cartItemDao.GetAllCartItemsByCartId(cart.Id);
        var cartItem = cartItems.FirstOrDefault(cartItem => cartItem.ProductId == product.Id);

        cartItemDao.RemoveCartItem(cartItem.Id);
    }

    public void AddProductPriceToTotal(Cart cart, decimal productPrice)
    {
        var newTotalPrice = cart.TotalPrice + productPrice;
        cartDao.UpdateTotalPrice(cart.Id, newTotalPrice);
    }

    public void SubtractProductPriceFromTotal(Cart cart, decimal productPrice)
    {
        var newTotalPrice = cart.TotalPrice - productPrice;
        cartDao.UpdateTotalPrice(cart.Id, newTotalPrice);
    }
}